using System.IdentityModel.Tokens.Jwt;

namespace GAP.Api.Authentication
{
    public class TokenResponse
    {
        public JwtSecurityToken JwtSecurityToken { get; set; }
        public string Exception { get; set; }
        public bool Authorized => JwtSecurityToken != null;
    }
}
