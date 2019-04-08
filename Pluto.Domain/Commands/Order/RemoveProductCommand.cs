using Pluto.Domain.Commands.Common;
using System;

namespace Pluto.Domain.Commands.Order
{
    public class RemoveProductCommand : Command
    {
        public Guid ProductId { get; set; }
        public Guid UserId { get; set; }
    }
}
