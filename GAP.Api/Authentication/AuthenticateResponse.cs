using GAP.Api.Models;

namespace GAP.Api.Authentication
{
    public class AuthenticateResponse : BaseResponse
    {
        public User UserDetails { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public string Token { get; set; }
        public DateTime Expires { get; set; }
        public DateTime Created { get; set; }
    }
}
