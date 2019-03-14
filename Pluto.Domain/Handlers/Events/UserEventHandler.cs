using MediatR;
using Pluto.Domain.Events.User;
using System.Threading;
using System.Threading.Tasks;

namespace Pluto.Domain.Handlers.Events
{
    public class UserEventHandler : INotificationHandler<CreateUserEvent>, INotificationHandler<UpdateUserEvent>, INotificationHandler<DeleteUserEvent>, INotificationHandler<ChangeUserPasswordEvent>
    {
        public async Task Handle(CreateUserEvent notification, CancellationToken cancellationToken)
        {
            // -> Send some greetings e-mail
        }

        public Task Handle(UpdateUserEvent notification, CancellationToken cancellationToken) => Task.CompletedTask;

        public Task Handle(DeleteUserEvent notification, CancellationToken cancellationToken) => Task.CompletedTask;

        public Task Handle(ChangeUserPasswordEvent notification, CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
