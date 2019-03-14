using Pluto.Domain.Commands.Common;

namespace Pluto.Domain.Commands.Product
{
    public class CreateProductCommand : Command
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string[] PicturesUrls { get; set; }
    }
}
