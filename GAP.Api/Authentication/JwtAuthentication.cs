using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using GAP.Api.Models;
using GAP.Core;
using Microsoft.IdentityModel.Tokens;

namespace GAP.Api.Authentication
{
    public interface IJwtAuthentication
    {
        AuthenticateResponse? GenerateWebToken(User user);
        string ValidateRefreshToken(AuthenticateRequest userRequest);
        TokenResponse ValidateToken(string tokenText);
    }
    /// <summary>
    /// 
    /// </summary>
    public class JwtAuthentication : IJwtAuthentication
    {
        private readonly WebToken _webToken;
        private readonly JwtSecurityTokenHandler _tokenHandler;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="webToken"></param>
        public JwtAuthentication(WebToken webToken)
        {
            _webToken = webToken;
            _tokenHandler = new JwtSecurityTokenHandler();
        }

        /// <summary>
        /// Generate Json Web Token
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public AuthenticateResponse? GenerateWebToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_webToken.SecretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var roleList = Roles.FromUser(user).Select(role => role).ToList();

            var refreshToken = CreateRefreshToken();

            var claims = new List<Claim>()
            {
                new(JwtRegisteredClaimNames.Sub, user.DisplayName),
                new("userId", user.Id.ToString()),
                new("email", user.Email),
                new("refreshToken", refreshToken.Token),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            foreach (var role in roleList)
            {
                claims.Add(new Claim("role", role));
            }

            var token = new JwtSecurityToken(
                _webToken.Issuer,
                _webToken.Audience,
                claims,
                expires: DateTime.UtcNow.AddHours(_webToken.ExpirationHours),
                signingCredentials: credentials
            );

            try
            {
                var securityToken = new JwtSecurityTokenHandler().WriteToken(token);
                var bearerToken = new AuthenticateResponse
                {
                    Expires = refreshToken.Expires,
                    Created = refreshToken.Created,
                    Token = securityToken,
                    RefreshToken = refreshToken.Token
                };

                return bearerToken;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// validate the token and get the associated user
        /// </summary>
        /// <param name="userRequest"></param>
        /// <returns></returns>
        public string ValidateRefreshToken(AuthenticateRequest userRequest)
        {
            if (!_tokenHandler.CanReadToken(userRequest.AccessToken)) throw new SecurityTokenValidationException();

            var securityToken = ValidateToken(userRequest.AccessToken);

            var identity = new ClaimsIdentity(securityToken.JwtSecurityToken.Claims, "Bearer");
            var principal = new ClaimsPrincipal(identity);

            var refreshTokenClaim = principal.Claims.First(c => c.Type == "refreshToken");
            if (!refreshTokenClaim.Value.Contains(userRequest.RefreshToken)) return string.Empty;

            var badgeId = principal.Claims.First(c => c.Type == "BadgeId");

            var claimIdentity = principal.Identity as ClaimsIdentity;

            claimIdentity?.RemoveClaim(refreshTokenClaim);
            claimIdentity?.RemoveClaim(badgeId);

            return badgeId.Value;

        }

        /// <summary>
        /// Validating the current token
        /// </summary>
        /// <param name="tokenText"></param>
        /// <returns></returns>
        public TokenResponse ValidateToken(string tokenText)
        {
            SymmetricSecurityKey securityKey;
            SecurityToken securityToken;

            try
            {
                if (!_tokenHandler.CanReadToken(tokenText))
                {
                    return new TokenResponse()
                    {
                        Exception = "Issue Reading Token."
                    };
                }

                securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_webToken.SecretKey));
                
                _tokenHandler.ValidateToken(tokenText, new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidIssuer = _webToken.Issuer,
                    
                    ValidateAudience = false,
                    ValidAudience = _webToken.Audience,

                    ValidateIssuerSigningKey = false,
                    IssuerSigningKey = securityKey,

                     ValidateLifetime = false
                }, out securityToken);

            }
            catch (Exception e)
            {
                return new TokenResponse()
                {
                    Exception = e.Message
                };
            }

            return new TokenResponse()
            {
                JwtSecurityToken = (JwtSecurityToken)securityToken
            };
        }

        /// <summary>
        /// Create random number token to refresh the bearer
        /// </summary>
        /// <returns></returns>
        private AuthenticateResponse CreateRefreshToken()
        {
            var randomNumber = new byte[32];
            var refreshToken = new AuthenticateResponse();
            var generator = RandomNumberGenerator.Create();
            generator.GetBytes(randomNumber);
            refreshToken.Token = Convert.ToBase64String(randomNumber);
            refreshToken.Expires = DateTime.UtcNow.AddHours(_webToken.ExpirationHours);
            refreshToken.Created = DateTime.UtcNow;
            return refreshToken;
        }
    }
}
