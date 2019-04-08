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

        public Order(User customer)
        {
            Editor = Customer = customer;

            Ticket = Guid.NewGuid().ToString("N").Substring(0, 10);
            Items = new List<OrderItem>();
            Status = OrderStatus.Opened;
            Created = DateTime.UtcNow;
        }

        public virtual string Ticket { get; private set; }
        public virtual DateTime Created { get; private set; }
        public virtual DateTime? Commited { get; private set; }
        public virtual DateTime? Approved { get; private set; }
        public virtual DateTime? Canceled { get; private set; }
        public virtual User Customer { get; private set; }
        public virtual User Editor { get; private set; }
        public virtual OrderStatus Status { get; private set; }
        public virtual ICollection<OrderItem> Items { get; private set; }

        public void AddItem(OrderItem item)
        {
            var currentItem = Items.FirstOrDefault(e => e.Product.Id == item.Product.Id);

            if (currentItem != null)
            {
                currentItem.ChangeQuantity(item.Quantity);
                return;
            }

            Items.Add(item);
        }

        public void RemoveItemByProductId(Guid productId)
        {
            var item = Items.FirstOrDefault(e => e.Product.Id == productId);

            if (item != null)
                Items.Remove(item);
        }

        public decimal GetTotal() => Items.Sum(e => e.GetTotal());

        public void Commit(User editor)
        {
            if (Status != OrderStatus.Opened && IsValidEditor(editor))
                return;

            Status = OrderStatus.Commited;
            Commited = DateTime.UtcNow;
            Editor = editor;
        }

        public void Approve(User editor)
        {
            if (Status != OrderStatus.Commited && editor.IsAdmin())
                return;

            Status = OrderStatus.Approved;
            Approved = DateTime.UtcNow;
            Editor = editor;
        }

        public void Cancel(User editor)
        {
            if (Status == OrderStatus.Approved && IsValidEditor(editor))
                return;

            Status = OrderStatus.Canceled;
            Canceled = DateTime.UtcNow;
            Editor = editor;
        }

        private bool IsValidEditor(User editor) => editor.Id == Customer.Id || editor.IsAdmin();
    }
}
