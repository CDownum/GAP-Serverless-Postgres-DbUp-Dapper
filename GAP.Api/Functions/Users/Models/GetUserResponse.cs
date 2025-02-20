using GAP.Api.Models;

namespace GAP.Api.Functions.Users.Models
{
    public class GetUserResponse : BaseResponse
    {
        public User User { get; set; }
    }
}
