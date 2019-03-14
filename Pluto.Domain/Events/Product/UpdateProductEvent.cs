using Pluto.Domain.Events.Common;
using System;

namespace Pluto.Domain.Events.Product
{
    public class UpdateProductEvent : Event
    {
        public UpdateProductEvent(Guid id, string name, string description, decimal price)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;

            AggregateId = Id;
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
    }
}
