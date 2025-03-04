using AutoMapper;
using GAP.Api.Functions.SalesGoals.Models;
using GAP.Api.Models;
using GAP.Core.Database.Repository;

namespace GAP.Api.Functions.SalesGoals.Services
{
    public interface ISalesGoalsService
    {
        Task<GetSalesGoalsResponse> GetSalesGoalByUserByYear(int companyId, Guid userId, int year);
    }

    public class SalesGoalsService(ISalesGoalRepository repository, IMapper mapper) : ISalesGoalsService
    {
        public async Task<GetSalesGoalsResponse> GetSalesGoalByUserByYear(int companyId, Guid userId, int year)
        {
            var response = new GetSalesGoalsResponse();

            var goal = mapper.Map<SalesGoal>(await repository.GetSalesGoalsByUserByYear(companyId, userId, year));

            response.SalesGoal = goal;
            return response;
        }
    }
}
