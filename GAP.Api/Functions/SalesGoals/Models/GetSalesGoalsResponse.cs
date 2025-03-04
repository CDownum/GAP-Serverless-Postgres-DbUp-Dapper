using GAP.Api.Models;

namespace GAP.Api.Functions.SalesGoals.Models
{
    public class GetSalesGoalsResponse : BaseResponse
    {
        public SalesGoal SalesGoal { get; set; }
    }
}
