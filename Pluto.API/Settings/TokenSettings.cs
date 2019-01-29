namespace Pluto.API.Settings
{
    public class TokenSettings
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int Timeout { get; set; }
        public int FinalExpiration { get; set; }
    }
}
