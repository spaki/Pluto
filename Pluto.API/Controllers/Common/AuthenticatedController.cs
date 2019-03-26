using MediatR;
using Pluto.API.Auth;
using Pluto.Domain.Bus;
using Pluto.Domain.Notifications;
using System;
using System.Security.Claims;

namespace Pluto.API.Controllers.Common
{
    [BaererAuthorize]
    public abstract class AuthenticatedController : BaseController
    {
        protected AuthenticatedController(IMediatorHandler mediator, IMediatorHandler bus, INotificationHandler<DomainNotification> notifications) : base(mediator, bus, notifications)
        {
        }

        protected Guid UserId
        {
            get => Guid.Parse(this.GetClaim(ClaimTypes.NameIdentifier));
        }

        protected bool IsAdmin
        {
            get => this.GetClaim(ClaimTypes.Role) == "Admin";
        }
    }
}
