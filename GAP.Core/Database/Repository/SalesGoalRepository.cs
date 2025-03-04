using System.Data;
using Dapper;
using GAP.Core.Domain;

namespace GAP.Core.Database.Repository;

public interface ISalesGoalRepository
{
    Task<SalesGoal?> GetSalesGoalsByUserByYear(int companyId, Guid userId, int year, IDbConnection? existingConn = null);
}

public partial class GapRepository
{
    public async Task<SalesGoal?> GetSalesGoalsByUserByYear(int companyId, Guid userId, int year, IDbConnection? existingConn = null)
    {
        var conn = existingConn ?? SqlWrapper.Connection;

        try
        {
            var parameters = new DynamicParameters();
            parameters.Add("@CompanyId", companyId);
            parameters.Add("@UserId", userId);
            parameters.Add("@Year", year);

            const string sql = """
                               SELECT 
                                   sg.id As Id, sg.year As year, sg.average_sales_price As AverageSalesPrice, sg.commission_rate As CommissionRate, sg.average_commision As AverageCommision, sg.average_loss_ratio As AverageLossRatio,
                                        sg.net_sales_closed As NetSalesClosed, sg.net_sales_needed As NetSalesNeeded, sg.gross_sales_needed As GrossSalesNeeded, sg.last_modified As LastModified, 
                                   sgq.id As Id, sg.gross_sales_needed As SalesGoalGrossNeeded, sgq.quarter As Quarter, sgq.gross_sales_needed As GrossSalesNeeded, sgq.referral As Referral, sgq.self_originating As SelfOriginating, sgq.internet As Internet,
                                        sgq.realtor As Realtor, sgq.walk_in As WalkIn, sgq.follow_up As FollowUp, sgq.last_modified As LastModified, 
                                   u.id As Id, 
                                       u.first_name AS FirstName, 
                                       u.last_name AS LastName, 
                                       u.email AS Email, 
                                       u.start_date AS StartDate, 
                                       u.end_date AS EndDate, 
                                       u.gross_annual_income AS GrossAnnualIncome, 
                                       u.reporting_manager_id AS ReportingManager, 
                                       u.salaried AS Salaried, 
                                       u.date_of_birth AS DateOfBirth, 
                                       u.salutation AS Salutation, 
                                       u.address1 AS Address1, 
                                       u.address2 AS Address2, 
                                       u.city AS City, 
                                       u.state AS State, 
                                       u.zip_code AS ZipCode, 
                                       u.work_phone AS WorkPhone, 
                                       u.cell AS Cell,
                                   c.id AS Id,
                                       c.name AS Name,
                                       c.last_modified AS LastModified,
                                       c.created_date AS CreatedDate,
                                       c.enabled AS Enabled
                               FROM gap.sales_goals sg
                               INNER JOIN gap.sales_goals_quarters sgq ON sg.Id = sgq.sales_goals_id
                               INNER JOIN gap.users u ON sg.user_id = u.Id
                               INNER JOIN gap.companies c ON u.company_id = c.Id
                               WHERE u.company_id = @CompanyId AND sg.user_id = @UserId AND sg.Year = @Year
                               """;

            var salesGoalDictionary = new Dictionary<Guid, SalesGoal>();

            var result = await conn.QueryAsync<SalesGoal, SalesGoalQuarter, User, Company, SalesGoal>(
                sql,
                (sg, sgq, u, c) =>
                {
                    if (!salesGoalDictionary.TryGetValue(sg.Id, out var salesGoalEntry))
                    {
                        salesGoalEntry = sg;
                        salesGoalEntry.User = u;
                        salesGoalEntry.User.Company = c;
                        salesGoalEntry.SalesGoalQuarters = [];
                        salesGoalDictionary.Add(sg.Id, salesGoalEntry);
                    }

                    salesGoalEntry.SalesGoalQuarters.Add(sgq);
                    return salesGoalEntry;
                },
                parameters,
                splitOn: "Id,Id,Id,Id"
            );

            return result.FirstOrDefault();
        }
        catch (Exception ex)
        {
            _log.Error($"Get User failed with the following exception: {ex.Message}");
            return null;
        }
        finally
        {
            if (existingConn == null)
            {
                conn.Dispose();
            }
        }
    }

}