using MediatR;
using Microsoft.AspNetCore.Mvc;
using Pluto.API.Controllers.Common;
using Pluto.Domain.Bus;
using Pluto.Domain.Commands.User;
using Pluto.Domain.Interfaces.Repositories;
using Pluto.Domain.Notifications;
using System.Threading.Tasks;

namespace Pluto.API.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserRepository userRepository;

        public UserController(IMediatorHandler mediator, IMediatorHandler bus, INotificationHandler<DomainNotification> notifications, IUserRepository userRepository) : base(mediator, bus, notifications)
        {
            this.userRepository = userRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Response(userRepository.Query());
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CreateUserCommand command)
        {
            await bus.SendAsync(command);
            return Response();
        }
    }
}
