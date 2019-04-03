using Microsoft.AspNetCore.Authorization;
using Pluto.Domain.Enums;
using System.Linq;

namespace Pluto.API.Auth
{
    public class BaererAuthorizeAttribute : AuthorizeAttribute
    {
        public BaererAuthorizeAttribute() : base("Bearer")
        {
        }

        public BaererAuthorizeAttribute(params UserProfile[] profiles) : base("Bearer") => this.Roles = string.Join(",", profiles.Select(e => e.ToString()).ToArray());
    }
}
