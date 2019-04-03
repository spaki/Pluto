using Pluto.Domain.Events.Common;
using System;

namespace Pluto.Domain.Events.Order
{
    public class CreateOrderEvent : Event
    {
        public CreateOrderEvent(Guid id, string ticket, Models.User user)
        {
            Id = id;
            Ticket = ticket;
            User = user;

            AggregateId = Id;
        }

        public Guid Id { get; private set; }
        public string Ticket { get; private set; }
        public Models.User User { get; private set; }
    }
}
