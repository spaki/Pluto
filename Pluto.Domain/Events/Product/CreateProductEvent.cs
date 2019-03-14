using Pluto.Domain.Events.Common;
using System;

namespace Pluto.Domain.Events.Product
{
    public class CreateProductEvent : Event
    {
        public CreateProductEvent(Guid id, string name, string description, decimal price, string[] pictures)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            Pictures = pictures;

            AggregateId = Id;
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public string[] Pictures { get; private set; }
    }
}
