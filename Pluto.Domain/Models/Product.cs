using Pluto.Domain.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;

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
            Code = Guid.NewGuid().ToString("N").Substring(0, 8);
        }

        public virtual string Code { get; private set; }
        public virtual string Name { get; private set; }
        public virtual string Description { get; private set; }
        public virtual decimal Price { get; private set; }
        public virtual ICollection<PictureUrl> Pictures { get; private set; }

        public void AddPicture(string url)
        {
            if(!Pictures.Any(e => e.Url == url))
                Pictures.Add(new PictureUrl(url));
        }

        public void UpdateInfo(string name, string description, decimal price)
        {
            Name = name;
            Description = description;
            Price = price;
        }

        public string[] GetPictureUrls() => Pictures?.Select(e => e.Url).ToArray();
    }
}
