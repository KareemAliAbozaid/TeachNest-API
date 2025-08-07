namespace TechNest.Domain.Entites.Product
{
    public class Category:BaseEntity<int>
    {
        public string Name { get; set; }
        public string? Description { get; set; }

    }
}
