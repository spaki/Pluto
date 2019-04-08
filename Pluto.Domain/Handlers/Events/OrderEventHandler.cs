using MediatR;
using Pluto.Domain.Events.Order;
using System.Threading;
using System.Threading.Tasks;

namespace Pluto.Domain.Handlers.Events
{
    public class OrderEventHandler : 
        INotificationHandler<CreateOrderEvent>, 
        INotificationHandler<AddProductEvent>, 
        INotificationHandler<RemoveProductEvent>,
        INotificationHandler<ApproveOrderEvent>,
        INotificationHandler<CommitOrderEvent>,
        INotificationHandler<CancelOrderEvent>
    {
        public Task Handle(CreateOrderEvent notification, CancellationToken cancellationToken) => Task.CompletedTask;
        public Task Handle(AddProductEvent notification, CancellationToken cancellationToken) => Task.CompletedTask;
        public Task Handle(RemoveProductEvent notification, CancellationToken cancellationToken) => Task.CompletedTask;
        public Task Handle(ApproveOrderEvent notification, CancellationToken cancellationToken) => Task.CompletedTask;
        public Task Handle(CommitOrderEvent notification, CancellationToken cancellationToken) => Task.CompletedTask;
        public Task Handle(CancelOrderEvent notification, CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
