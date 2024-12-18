using System.ComponentModel.DataAnnotations;

namespace Talabat.Api.Dtos
{
    public class CartItemDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        [Range(0.1, double.MaxValue , ErrorMessage = "Price Must be Greater Than 0.1 !")]
        public decimal Price { get; set; }
        [Required]
        public string PictureUrl { get; set; }
        [Required]
        [Range(1,int.MaxValue , ErrorMessage = "Quantitny Must Be Atlease 1 !")]
        public int Quantity { get; set; }
        [Required]
        public string Brand { get; set; }
        [Required]
        public string Category { get; set; }
    }
}