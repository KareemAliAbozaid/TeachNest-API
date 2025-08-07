using System.ComponentModel.DataAnnotations.Schema;

namespace TechNest.Domain.Entites.Product
{
    public class ProductImage:BaseEntity<int>
    {
        public string ImageName { get; set; } = string.Empty;

        [ForeignKey(nameof(ProductId))]
        public int ProductId { get; set; }

    }

}
