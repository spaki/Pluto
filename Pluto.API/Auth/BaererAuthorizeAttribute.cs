using Microsoft.AspNetCore.Authorization;

namespace Pluto.API.Auth
{
    public class BaererAuthorizeAttribute : AuthorizeAttribute
    {
        public BaererAuthorizeAttribute() : base("Bearer")
        {
        }
    }
}
