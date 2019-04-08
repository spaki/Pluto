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
using Pluto.Utils.Validations;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Pluto.Domain.Handlers.Commands
{
    public class OrderCommandHandler : CommandHandler, IRequestHandler<CreateOrderCommand, Order>, IRequestHandler<AddProductCommand>, IRequestHandler<RemoveProductCommand>
    {
        private readonly IOrderRepository orderRepository;
        private readonly IProductRepository productRepository;
        private readonly IUserRepository userRepository;

        public OrderCommandHandler(
            IUnitOfWork uow, 
            IMediatorHandler bus, 
            INotificationHandler<DomainNotification> notifications,
            IOrderRepository orderRepository,
            IUserRepository userRepository,
            IProductRepository productRepository
        ) : base(uow, bus, notifications)
        {
            this.orderRepository = orderRepository;
            this.productRepository = productRepository;
            this.userRepository = userRepository;
        }

        public async Task<Order> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var entity = await GetOpenenedOrderFromUser(request.UserId);

            if (entity != null)
                return entity;

            var user = await userRepository.GetAsync(request.UserId);
            entity = new Order(user);

            await orderRepository.AddAsync(entity);

            Commit();
            await bus.InvokeAsync(new CreateOrderEvent(entity.Id, entity.Ticket, entity.User));

            return entity;
        }

        public async Task<Unit> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            request.IsLessThan(e => e.Quantity, 0, async () => await bus.InvokeDomainNotificationAsync("Invalid quantity."));

            if(!IsValidOperation())
                return Unit.Value;

            var order = await GetOpenenedOrderFromUser(request.UserId);

            if (order == null) 
                return Unit.Value;

            var product = await productRepository.FirstOrDefaultAsync(e => e.Id == request.ProductId);

            if (product == null)
                return Unit.Value;

            var orderItem = new OrderItem(product, request.Quantity);
            order.AddItem(orderItem);
            await orderRepository.UpdateAsync(order);
            Commit();
            await bus.InvokeAsync(new AddProductEvent(orderItem));

            return Unit.Value;
        }

        public async Task<Unit> Handle(RemoveProductCommand request, CancellationToken cancellationToken)
        {
            var order = await GetOpenenedOrderFromUser(request.UserId);

            if (order == null)
                return Unit.Value;

            order.RemoveItemByProductId(request.ProductId);
            await orderRepository.UpdateAsync(order);
            Commit();
            await bus.InvokeAsync(new RemoveProductEvent(order.Id, request.ProductId));

            return Unit.Value;
        }

        private async Task<Order> GetOpenenedOrderFromUser(Guid userId) => await orderRepository.FirstOrDefaultAsync(e => e.User.Id == userId && e.Status == OrderStatus.Opened);
    }
}
