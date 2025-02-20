using System.Data;
using Dapper;
using GAP.Core.Domain;

namespace GAP.Core.Database.Repository;

public interface IUserRepository
{
    Task<IEnumerable<User>?> GetUsers(int companyId, IDbConnection? existingConn = null);

    Task<User?> GetUser(int companyId, Guid id, IDbConnection? existingConn = null);
}

public partial class GapRepository
{
    public async Task<IEnumerable<User>?> GetUsers(int companyId, IDbConnection? existingConn = null)
    {
        var conn = existingConn ?? SqlWrapper.Connection;

        try
        {
            var parameters = new DynamicParameters();
            parameters.Add("@CompanyId", companyId);

            const string sql = """
                               SELECT 
                                   u.id, 
                                   u.user_role_id As Role,
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
                                   u.cell AS Cell
                               FROM gap.users u
                               JOIN gap.companies c ON u.company_id = c.id
                               WHERE u.company_id = @CompanyId AND u.id = @Id
                               """;

            var users = await SqlWrapper.QueryAsync<User, Company, User>(
                conn,
                sql,
                (user, company) =>
                {
                    user.Company = company;
                    return user;
                },
                parameters,
                splitOn: "CompanyId"
            );

            return users;
        }
        catch (Exception ex)
        {
            _log.Error($"Get Users failed with the following exception: {ex.Message}");
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

    public async Task<User?> GetUser(int companyId, Guid id, IDbConnection? existingConn = null)
    {
        var conn = existingConn ?? SqlWrapper.Connection;

        try
        {
            var parameters = new DynamicParameters();
            parameters.Add("@CompanyId", companyId);
            parameters.Add("@Id", id);

            const string sql = """
                               SELECT 
                                   u.id, 
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
                                   c.id AS CompanyId,
                                   c.name AS CompanyName,
                                   c.last_modified AS CompanyLastModified,
                                   c.created_date AS CompanyCreatedDate,
                                   c.enabled AS CompanyEnabled
                               FROM gap.users u
                               JOIN gap.companies c ON u.company_id = c.id
                               WHERE u.company_id = @CompanyId AND u.id = @Id
                               """;

            var user = await SqlWrapper.QueryAsync<User, Company, User>(
                conn,
                sql,
                (user, company) =>
                {
                    user.Company = company;
                    return user;
                },
                parameters,
                splitOn: "CompanyId"
            );

            return user.FirstOrDefault();
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