using System.Data;
using Dapper;
using GAP.Core.Domain;

namespace GAP.Core.Database.Repository
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetUsers(int facilityId);

        Task<User> GetUser(int facilityId, string badgeId, string email = "", Guid? id = null, string pin = "", IDbConnection? existingConn = null);
    }

    public partial class GapRepository
    {
        //public async Task<User> GetUser(int facilityId, string badgeId, string email = "", Guid? id = null, string pin = "",
        //    IDbConnection? existingConn = null)
        //{
        //    var conn = existingConn ?? SqlWrapper.Connection;

        //    try
        //    {
        //        var whereClause = string.Empty;
        //        var parameters = new DynamicParameters();
        //        var facilitySelect = string.Empty;
        //        var joinClause = string.Empty;

        //        if (facilityId > 0)
        //        {
        //            facilitySelect = @",uf.facility_id as FacilityId, uf.failed_login_attempt_count As FailedLoginAttemptCount, uf.last_failed_login_attempt As LastFailedLoginAttempt, u.active_token_hash As ActiveTokenHash,
        //                         uf.grant_factory_access as GrantFactoryAccess,
        //                         uf.is_artwork_updater as IsArtworkUpdater, uf.grant_factory_access As GrantFactoryAccess,uf.is_factory_supervisor as IsFactorySupervisor";
        //            joinClause = "INNER JOIN mcp.users_facilities uf on u.id = uf.user_id and uf.facility_Id = @FacilityId";
        //            parameters.Add("@FacilityId", facilityId);
        //        }

        //        if (!string.IsNullOrEmpty(badgeId))
        //        {
        //            parameters.Add("@BadgeId", badgeId.Trim());
        //            whereClause = "badge_id = @BadgeId";

        //            if (!string.IsNullOrEmpty(pin))
        //            {
        //                parameters.Add("@Pin", pin);
        //                whereClause += "AND pin = @Pin";
        //            }
        //        }

        //        if (!string.IsNullOrEmpty(email))
        //        {
        //            parameters.Add("@email", email.ToLower().Trim());
        //            whereClause += " lower(email) = @Email";
        //        }

        //        if (id != null)
        //        {
        //            parameters.Add("@Id", id);
        //            whereClause += "  id = @Id";
        //        }

        //        var sql =
        //            $@"SELECT u.id, u.first_name As FirstName, u.last_name As LastName, concat(u.first_name ,' ', u.last_name) As UserName, u.pin As Pin,
        //                         u.job_title As JobTitle, u.last_modified As LastModified, u.portal_language_code as PortalLanguageCode, 
        //                         u.station_apps_language_code as StationAppsLanguageCode, u.badge_id As BadgeId, u.email As email, u.password As Password, u.created_date As CreatedDate,
        //                         u.active_token_hash As ActiveTokenHash,u.is_admin As IsAdmin
        //                        {facilitySelect}
        //                     FROM mcp.users u                                
        //                       {joinClause} 
        //                     where {whereClause}";

        //        if (facilityId > 0)
        //        {
        //            var users = await SqlWrapper.QueryAsync<User, UserFacility, User>(conn, sql, (u, uf) =>
        //                {
        //                    u.Facility = uf;
        //                    return u;
        //                }, parameters, splitOn: "Id, FacilityId");

        //            return users.FirstOrDefault();
        //        }
        //        else
        //        {
        //            var user = await SqlWrapper.QueryAsync<User>(conn, sql,parameters);
        //            return user.FirstOrDefault();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _log.Error($"Get User failed with the following exception: {ex.Message}");
        //        return null;
        //    }
        //    finally
        //    {
        //        if (existingConn == null)
        //        {
        //            conn.Dispose();
        //        }
        //    }
        //}

        //public async Task<IEnumerable<User>> GetUsers(int facilityId)
        //{
        //    using (var connection = SqlWrapper.Connection)
        //    {
        //        try
        //        {
        //            var parameters = new DynamicParameters();
        //            var facilitySelect = string.Empty;
        //            var joinClause = string.Empty;
        //            var whereClause = string.Empty;
        //            if (facilityId > 0)
        //            {
        //                facilitySelect = @",uf.facility_id as FacilityId, uf.user_id as UserId, uf.failed_login_attempt_count As FailedLoginAttemptCount, uf.last_failed_login_attempt As LastFailedLoginAttempt,  
        //                             uf.is_artwork_updater as IsArtworkUpdater, uf.grant_factory_access As GrantFactoryAccess, 
        //                            uf.is_factory_supervisor as IsFactorySupervisor";
        //                joinClause = "INNER JOIN mcp.users_facilities uf on u.id = uf.user_id and uf.facility_id = @facilityId";
        //                parameters.Add("@facilityId", facilityId);
        //            }
        //            else
        //            {
        //                whereClause = " where u.is_admin=true";
        //            }
        //            var sql =
        //                $@"SELECT u.id, u.first_name As FirstName, u.last_name As LastName, concat(u.first_name ,' ', u.last_name) As UserName, u.pin As Pin, u.job_title As JobTitle,
        //                            u.badge_id As BadgeId, u.email As email, u.password As Password,u.station_apps_language_code as StationAppsLanguageCode, u.created_date As CreatedDate, u.last_modified As LastModified,
        //                            u.active_token_hash As ActiveTokenHash, u.portal_language_code as PortalLanguageCode,u.is_admin As IsAdmin
        //                            {facilitySelect}                                                               
	       //             FROM mcp.users u
        //                {whereClause}
        //                {joinClause}";
        //            if (facilityId > 0)
        //            {
        //                return await SqlWrapper.QueryAsync<User, UserFacility, User>(connection, sql, (u, uf) =>
        //                 {
        //                     u.Facility = uf;
        //                     return u;
        //                 }, parameters, splitOn: "Id, FacilityId");
        //            }
        //            else
        //            {
        //                return await SqlWrapper.QueryAsync<User>(connection, sql);
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            _log.Error($"Get Users failed with the following exception: {ex.Message}");
        //            return null;
        //        }
        //    }
        //}
    }
}
