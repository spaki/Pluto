namespace Pluto.API.Auth
{
    public class AuthRequestDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string RefreshToken { get; set; }
    }
}
