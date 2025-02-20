
namespace GAP.Api
{
    public static class Routes
    {
        private const string Prefix = "api/";
        private const string Companies = Prefix + "companies";
        private const string Company = Companies + "/{companyId}";

        public const string Users = Company + "/users";
        public const string UsersById = Users + "/{userId}";
    }
}
