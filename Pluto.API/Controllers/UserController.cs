using MediatR;
using Microsoft.AspNetCore.Mvc;
using Pluto.API.Controllers.Common;
using Pluto.Domain.Bus;
using Pluto.Domain.Commands.User;
using Pluto.Domain.Interfaces.Repositories;
using Pluto.Domain.Notifications;
using System;
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

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Response(await userRepository.GetAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CreateUserCommand command)
        {
            await bus.SendAsync(command);
            return Response();
        }

        [HttpPut()]
        public async Task<IActionResult> Put([FromBody]UpdateUserCommand command)
        {
            await bus.SendAsync(command);
            return Response();
        }

        [HttpPut("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody]ChangeUserPasswordCommand command)
        {
            await bus.SendAsync(command);
            return Response();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await bus.SendAsync(new DeleteUserCommand(id));
            return Response();
        }
    }
}
