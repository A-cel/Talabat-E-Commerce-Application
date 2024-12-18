using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
namespace Talabat.Core.Entities
{
    public class Product:BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }
        [ForeignKey("BrandId")]
        public int BrandId { get; set; }
      [JsonIgnore]
        public ProductBrand Brand { get; set; }
        
        public int CategoryId { get; set; }
        [JsonIgnore]
        public ProductCategory Category { get; set; }
    }
}
