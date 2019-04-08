using MediatR;
using Microsoft.AspNetCore.Mvc;
using Pluto.API.Auth;
using Pluto.API.Controllers.Common;
using Pluto.Domain.Bus;
using Pluto.Domain.Commands.Order;
using Pluto.Domain.Enums;
using Pluto.Domain.Interfaces.Repositories;
using Pluto.Domain.Notifications;
using System;
using System.Threading.Tasks;

namespace Pluto.API.Controllers
{
    public class OrderController : AuthenticatedController
    {
        private readonly IOrderRepository orderRepository;
        private readonly IUserRepository userRepository;

        public OrderController(
            IMediatorHandler mediator, 
            IMediatorHandler bus, 
            INotificationHandler<DomainNotification> notifications,
            IOrderRepository orderRepository,
            IUserRepository userRepository
        ) : base(mediator, bus, notifications)
        {
            this.orderRepository = orderRepository;
            this.userRepository = userRepository;
        }

        // -> List all order jusy for admins
        // TODO: Paginate
        [HttpGet("All")]
        [BaererAuthorize(UserProfile.Admin)]
        public IActionResult GetAll()
        {
            return Response(orderRepository.Query());
        }

        // -> Get teh current user "bag".
        //      "Bag" is a Order with status of "Opened" (in this case, it can be a new order).
        [HttpGet()]
        public async Task<IActionResult> Get()
        {
            var result = await bus.RequestAsync(new CreateOrderCommand { UserId = UserId });
            return Response(result);
        }

        // -> List all user orders
        [HttpGet("My")]
        [BaererAuthorize(UserProfile.Admin)]
        public IActionResult GetMy()
        {
            return Response(orderRepository.Query(e => e.Customer.Id == UserId));
        }

        // -> Add or changes the quantity of a product from an order
        //      The UserId comes from authentication, this way we aways get the opened order ("bag") from the user.
        [HttpPost("Product")]
        public async Task<IActionResult> AddProduct(AddProductCommand command)
        {
            command.UserId = UserId;
            await bus.SendAsync(command);
            return Response();
        }

        // -> Remove a product from an order
        //      The UserId comes from authentication, this way we aways get the opened order ("bag") from the user.
        [HttpDelete("Product")]
        public async Task<IActionResult> RemoveProduct(RemoveProductCommand command)
        {
            command.UserId = UserId;
            await bus.SendAsync(command);
            return Response();
        }

        [HttpPatch("{id}/Approve")]
        [BaererAuthorize(UserProfile.Admin)]
        public async Task<IActionResult> Approve(Guid id)
        {
            await bus.SendAsync(new ApproveOrderCommand { OrderId  = id, UserId = UserId });
            return Response();
        }

        [HttpPatch("{id}/Cancel")]
        public async Task<IActionResult> Cancel(Guid id)
        {
            await bus.SendAsync(new CancelOrderCommand { OrderId = id, UserId = UserId });
            return Response();
        }

        [HttpPatch("{id}/Commit")]
        public async Task<IActionResult> Commit(Guid id)
        {
            await bus.SendAsync(new CommitOrderCommand { OrderId = id, UserId = UserId });
            return Response();
        }
    }
}
