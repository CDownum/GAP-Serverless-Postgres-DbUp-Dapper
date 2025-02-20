
namespace GAP.Api
{
    public static class Routes
    {
        private const string Prefix = "api/";
        private const string Companies = Prefix + "companies";
        private const string Company = Companies + "/{companyId}";

        public const string Users = Company + "/users";
        public const string UsersById = Users + "/{userId}";

        public const string Budgets = Company + "/budgets";
        public const string BudgetsByUserByYear = Budgets + "/users/{userId}/years/{year}";

        public const string SalesGoals = Company + "/salesGoals";
        public const string SalesGoalsByUserByYear = SalesGoals + "/users/{userId}/years/{year}";
    }
}
