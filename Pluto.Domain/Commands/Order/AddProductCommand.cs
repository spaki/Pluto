using Pluto.Domain.Commands.Common;
using System;

namespace Pluto.Domain.Commands.Order
{
    public class AddProductCommand : Command
    {
        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
