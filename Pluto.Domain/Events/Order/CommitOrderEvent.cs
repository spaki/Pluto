using Pluto.Domain.Events.Common;
using System;

namespace Pluto.Domain.Events.Order
{
    public class CommitOrderEvent : Event
    {
        public CommitOrderEvent(Guid orderId, Guid userId)
        {
            OrderId = orderId;
            UserId = userId;

            AggregateId = orderId;
        }

        public Guid OrderId { get; private set; }
        public Guid UserId { get; private set; }
    }
}
