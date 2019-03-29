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

            Ticket = Guid.NewGuid().ToString("N").Substring(0, 10);
            Items = new List<OrderItem>();
            OrderStatus = OrderStatus.Opened;
            Created = DateTime.UtcNow;
        }

        public virtual string Ticket { get; private set; }
        public virtual DateTime Created { get; private set; }
        public virtual DateTime? Commited { get; private set; }
        public virtual DateTime? Approved { get; private set; }
        public virtual DateTime? Canceled { get; private set; }
        public virtual User User { get; private set; }
        public virtual OrderStatus OrderStatus { get; private set; }
        public virtual ICollection<OrderItem> Items { get; private set; }

        public void AddItem(OrderItem item)
        {
            Items.Add(item);
        }

        public void RemoveItemByProductId(Guid productId)
        {
            var item = Items.FirstOrDefault(e => e.Product.Id == productId);

            if (item != null)
                Items.Remove(item);
        }

        public void ChangeQuantityByProductId(Guid productId, int newQuantity)
        {
            var item = Items.FirstOrDefault(e => e.Product.Id == productId);

            if (item != null)
                item.ChangeQuantity(newQuantity);
        }

        public decimal GetTotal() => Items.Sum(e => e.GetTotal());

        public void Commit()
        {
            OrderStatus = OrderStatus.Commited;
            Commited = DateTime.UtcNow;
        }

        public void Approve()
        {
            OrderStatus = OrderStatus.Approved;
            Approved = DateTime.UtcNow;
        }

        public void Cancel()
        {
            OrderStatus = OrderStatus.Canceled;
            Canceled = DateTime.UtcNow;
        }
    }
}
