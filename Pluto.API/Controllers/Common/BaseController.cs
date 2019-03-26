using MediatR;
using Microsoft.AspNetCore.Mvc;
using Pluto.Domain.Bus;
using Pluto.Domain.Notifications;
using System.Collections.Generic;
using System.Linq;

namespace Pluto.API.Controllers.Common
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        private readonly DomainNotificationHandler notifications;
        protected readonly IMediatorHandler mediator;
        protected readonly IMediatorHandler bus;

        protected BaseController(IMediatorHandler mediator, IMediatorHandler bus, INotificationHandler<DomainNotification> notifications)
        {
            this.bus = bus;
            this.mediator = mediator;
            this.notifications = (DomainNotificationHandler)notifications;
        }

        protected IEnumerable<DomainNotification> Notifications => notifications.GetNotifications();

        protected bool IsValidOperation() => !notifications.HasNotifications();

        protected new IActionResult Response(object result = null)
        {
            if (IsValidOperation())
            {
                if (result == null)
                    return Ok();

                return Ok(result);
            }

            return BadRequest(Notifications);
        }

        protected string GetClaim(string claimType) => User?.Claims?.FirstOrDefault(x => x.Type == claimType)?.Value;
    }
}
