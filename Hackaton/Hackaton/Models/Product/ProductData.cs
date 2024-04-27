using Hackaton.Data.Enums;

namespace Hackaton.Models.Product
{
    public class ProductData
    {
        public int Id { get; set; }

        public string Author { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

    }
}
