using MediatR;
using Pluto.Domain.Bus;
using Pluto.Domain.Commands.Order;
using Pluto.Domain.Enums;
using Pluto.Domain.Events.Order;
using Pluto.Domain.Handlers.Common;
using Pluto.Domain.Interfaces.Repositories;
using Pluto.Domain.Interfaces.Repositories.Common;
using Pluto.Domain.Models;
using Pluto.Domain.Notifications;
using System.Threading;
using System.Threading.Tasks;

namespace Pluto.Domain.Handlers.Commands
{
    public class OrderCommandHandler : CommandHandler, IRequestHandler<CreateOrderCommand, Order>
    {
        private readonly IOrderRepository orderRepository;
        private readonly IUserRepository userRepository;

        public OrderCommandHandler(
            IUnitOfWork uow, 
            IMediatorHandler bus, 
            INotificationHandler<DomainNotification> notifications,
            IOrderRepository orderRepository,
            IUserRepository userRepository
        ) : base(uow, bus, notifications)
        {
            this.orderRepository = orderRepository;
            this.userRepository = userRepository;
        }

        public async Task<Order> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var entity = await orderRepository.FirstOrDefaultAsync(e => e.User.Id == request.UserId && e.Status == OrderStatus.Opened);

            if (entity != null)
                return entity;

            var user = await userRepository.GetAsync(request.UserId);
            entity = new Order(user);

            await orderRepository.AddAsync(entity);

            Commit();
            await bus.InvokeAsync(new CreateOrderEvent(entity.Id, entity.Ticket, entity.User));

            return entity;
        }
    }
}
