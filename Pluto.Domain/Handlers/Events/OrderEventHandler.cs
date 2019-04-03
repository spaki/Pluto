using MediatR;
using Pluto.Domain.Events.Order;
using System.Threading;
using System.Threading.Tasks;

namespace Pluto.Domain.Handlers.Events
{
    public class OrderEventHandler : INotificationHandler<CreateOrderEvent>
    {
        public Task Handle(CreateOrderEvent notification, CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
