using Pluto.Domain.Models.Common;

namespace Pluto.Domain.Models
{
    public class OrderItem : Entity
    {
        // -> Empty contructor for EF
        public OrderItem()
        {

        }

        public OrderItem(Product product, int quantity)
        {
            Product = product;
            Quantity = quantity;
        }

        public virtual Product Product { get; private set; }
        public virtual int Quantity { get; private set; }

        public decimal ChangeQuantity(int newQuantity) => Quantity = newQuantity;
        public decimal GetTotal() => Quantity * Product.Price;
    }
}