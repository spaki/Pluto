using Pluto.Domain.Enums;
using Pluto.Domain.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pluto.Domain.Models
{
    public class Order : Entity
    {
        // -> Empty contructor for EF
        public Order()
        {

        }

        public Order(User user)
        {
            User = user;

            Ticket = Guid.NewGuid().ToString("N").Substring(0, 8);
            Items = new List<OrderItem>();
            OrderStatus = OrderStatus.Registered;
            Date = DateTime.UtcNow;
        }

        public virtual string Ticket { get; private set; }
        public virtual DateTime Date { get; private set; }
        public virtual User User { get; private set; }
        public virtual OrderStatus OrderStatus { get; private set; }
        public virtual ICollection<OrderItem> Items { get; private set; }

        public void AddItem(OrderItem item)
        {
            Items.Add(item);
        }

        public decimal GetTotal() => Items.Sum(e => e.GetTotal());

        public void Approve() => OrderStatus = OrderStatus.Approved;

        public void Cancel() => OrderStatus = OrderStatus.Canceled;
    }
}
