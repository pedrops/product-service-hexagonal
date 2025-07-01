﻿namespace ProductService.Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int SKU { get; set; }
        public string Currency { get; set; } = "USD";
    }
}
