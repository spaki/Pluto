using Pluto.Domain.Models.Common;
using System.Collections.Generic;

namespace Pluto.Domain.Models
{
    public class Product : Entity
    {
        // -> Empty contructor for EF
        public Product()
        {

        }

        public Product(string name, string description, decimal price)
        {
            Name = name;
            Description = description;
            Price = price;

            Pictures = new List<PictureUrl>();
        }

        public virtual string Name { get; private set; }
        public virtual string Description { get; private set; }
        public virtual decimal Price { get; private set; }
        public virtual ICollection<PictureUrl> Pictures { get; private set; }

        public void AddPicture(string url)
        {
            Pictures.Add(new PictureUrl(url));
        }
    }
}
