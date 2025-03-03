using GAP.Api.Extensions;
using GAP.Api.Functions.Users.Services;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Serilog;

namespace GAP.Api.Functions.Users
{
    public class GetUser(IUserService userService)
    {
        private readonly ILogger _log = Log.Logger.ForContext<GetUser>();

        [Function(nameof(GetUser))]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = Routes.UsersById)] 
            HttpRequestData req, int companyId, Guid userId)
        {
            //var validateResponse = req.ValidateAuthorization(_authentication);
            //if (!validateResponse.Authorized) return req.CreateResponse(HttpStatusCode.Unauthorized);
        
            var users = await userService.GetUser(companyId, userId);

            return await req.MapResponse(users, _log);
        }
    }
}
