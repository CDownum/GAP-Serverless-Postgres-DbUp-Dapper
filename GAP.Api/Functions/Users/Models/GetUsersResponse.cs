using GAP.Api.Models;

namespace GAP.Api.Functions.Users.Models
{
    public class GetUsersResponse : BaseResponse
    {
        public IEnumerable<User> Users { get; set; }
    }
}
