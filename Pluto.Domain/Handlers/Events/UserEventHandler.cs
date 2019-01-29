using MediatR;
using Pluto.Domain.Events;
using System.Threading;
using System.Threading.Tasks;

namespace Pluto.Domain.Handlers.Events
{
    public class UserEventHandler : INotificationHandler<CreateUserEvent>
    {
        public async Task Handle(CreateUserEvent notification, CancellationToken cancellationToken)
        {
            // -> Send some greetings e-mail
        }
    }
}
