using System.Text.Json.Serialization;
namespace Talabat.Core.Entities
{
    public class ProductBrand :BaseEntity
    {
        public string Name { get; set; }

        //public ICollection<Product> Products { get; set; } = new HashSet<Product>();
        [JsonIgnore]
        public ICollection<Product> products { get; set; } = new HashSet<Product>();
    }
}
