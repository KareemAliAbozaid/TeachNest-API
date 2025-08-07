﻿

using Microsoft.AspNetCore.Http;

namespace TechNest.Domain.DTOs.ProductDto
{
    public class CreateProductDto
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal NewPrice { get; set; }
        public decimal OldPrice { get; set; }
        public int CategoryId { get; set; }
        public IFormFileCollection Image { get; set; }
    }
}
