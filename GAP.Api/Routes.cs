using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAP.Api
{
    public static class Routes
    {
        private const string Prefix = "api/";

        public const string Users = Prefix + "users";
        public const string UsersById = Prefix + "users/{userId}";
    }
}
