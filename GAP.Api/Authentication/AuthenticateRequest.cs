using Newtonsoft.Json;

namespace GAP.Api.Authentication
{
    public class AuthenticateRequest
    {
        [JsonConstructor]
        public AuthenticateRequest(int facilityId, string userName, string email, string password, string pin, bool initialSignOn, string badgeId)
        {
            FacilityId = facilityId;
            UserName = userName;
            Email = email;
            Password = password;
            Pin = pin;
            InitialSignOn = initialSignOn;
            BadgeId = badgeId;
        }

        public AuthenticateRequest(int facilityId, string email, string password, string username = "")
        {
            FacilityId = facilityId;
            Email = email;
            UserName = username == string.Empty ? email : username;
            Password = password;
        }

        public AuthenticateRequest(string badgeId, string pin, int facilityId)
        {
            FacilityId = facilityId;
            Pin = pin;
            BadgeId = badgeId;
        }

        public bool InitialSignOn { get; }
        public int FacilityId { get; }
        public string BadgeId { get; }
        public string Pin { get; }
        public string Email { get; }
        public string UserName { get; }
        public string Password { get; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
