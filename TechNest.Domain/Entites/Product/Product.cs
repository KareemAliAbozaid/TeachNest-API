using System.ComponentModel.DataAnnotations.Schema;

namespace TechNest.Domain.Entites.Product
{
    public class Product:BaseEntity<int>
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal NewPrice { get; set; }
        public decimal OldPrice { get; set; }
        [ForeignKey(nameof(CategoryId))]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; } = default!;
       public virtual List<ProductImage> ProductImages { get; set; }

    }

}
