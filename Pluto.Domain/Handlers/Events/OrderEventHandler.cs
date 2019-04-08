using MediatR;
using Pluto.Domain.Events.Order;
using System.Threading;
using System.Threading.Tasks;

namespace Pluto.Domain.Handlers.Events
{
    public class OrderEventHandler : INotificationHandler<CreateOrderEvent>, INotificationHandler<AddProductEvent>, INotificationHandler<RemoveProductEvent>
    {
        public Task Handle(CreateOrderEvent notification, CancellationToken cancellationToken) => Task.CompletedTask;
        public Task Handle(AddProductEvent notification, CancellationToken cancellationToken) => Task.CompletedTask;
        public Task Handle(RemoveProductEvent notification, CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
