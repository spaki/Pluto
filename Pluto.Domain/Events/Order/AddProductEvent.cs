using Pluto.Domain.Events.Common;

namespace Pluto.Domain.Events.Order
{
    public class AddProductEvent : Event
    {
        public AddProductEvent(Models.OrderItem item)
        {
            Item = item;

            AggregateId = item.Id;
        }

        public Models.OrderItem Item { get; private set; }
    }
}
