using GAP.Api.Extensions;
using GAP.Api.Functions.Users.Helpers;
using GAP.Api.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Serilog;

namespace GAP.Api.Functions.Users
{
    public class GetUsers(IUserService userService)
    {
        private readonly ILogger _log = Log.Logger.ForContext<GetUsers>();

        [Function(nameof(GetUsers))]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = Routes.Users)] HttpRequestData req, int companyId)
        {
            //var validateResponse = req.ValidateAuthorization(_authentication);
            //if (!validateResponse.Authorized) return req.CreateResponse(HttpStatusCode.Unauthorized);
        
            var users = await userService.GetUsers(companyId);

            return await req.MapResponse(users, _log);
        }
    }
}
