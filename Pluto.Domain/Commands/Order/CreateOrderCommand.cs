using Pluto.Domain.Commands.Common;
using System;

namespace Pluto.Domain.Commands.Order
{
    public class CreateOrderCommand : RequestCommand<Models.Order>
    {
        public Guid UserId { get; set; }
    }
}
