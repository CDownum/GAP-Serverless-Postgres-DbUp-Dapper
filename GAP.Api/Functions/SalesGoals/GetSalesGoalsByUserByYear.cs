using GAP.Api.Extensions;
using GAP.Api.Functions.SalesGoals.Services;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Serilog;

namespace GAP.Api.Functions.SalesGoals
{
    public class GetSalesGoalsByUserByYear(ISalesGoalsService service)
    {
        private readonly ILogger _log = Log.Logger.ForContext<GetSalesGoalsByUserByYear>();

        [Function(nameof(GetSalesGoalsByUserByYear))]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = Routes.SalesGoalsByUserByYear)] 
            HttpRequestData req, int companyId, Guid userId, int year)
        {
            //var validateResponse = req.ValidateAuthorization(_authentication);
            //if (!validateResponse.Authorized) return req.CreateResponse(HttpStatusCode.Unauthorized);
        
            var response = await service.GetSalesGoalByUserByYear(companyId, userId, year);

            return await req.MapResponse(response, _log);
        }
    }
}
