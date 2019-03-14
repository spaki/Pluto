using Pluto.Domain.Events.Common;
using System;

namespace Pluto.Domain.Events.Product
{
    public class DeleteProductEvent : Event
    {
        public DeleteProductEvent(Guid id)
        {
            Id = id;

            AggregateId = Id;
        }

        public Guid Id { get; private set; }
    }
}
