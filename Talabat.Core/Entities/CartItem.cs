﻿namespace Talabat.Core.Entities
{
    public class CartItem
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public string PictureUrl { get; set; }
        public int Quantity { get; set; }
        public string Brand { get; set; }
        public string Category { get; set; }
    }
}