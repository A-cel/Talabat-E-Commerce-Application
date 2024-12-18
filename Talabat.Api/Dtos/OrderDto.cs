using System.ComponentModel.DataAnnotations;
using Talabat.Core.Entities.Order_Aggregate;

namespace Talabat.Api.Dtos
{
    public class OrderDto
    {
        [Required]
        public string buyeremail { get; set; }
        [Required]
        public AddressDto shippingaddress { get; set; }
        [Required]
        public string basketid {  get; set; }
        [Required]
        //[Range(1 , int.MaxValue)]
        public int Deliverymethodid { get; set; }

    }
}
