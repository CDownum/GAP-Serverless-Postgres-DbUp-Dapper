namespace GAP.Core
{
    public class DbConnectionSettings
    {
        public string Database { get; set; }
    }

    public class WebToken
    {
        public string SecretKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int ExpirationHours { get; set; }

    }
}
