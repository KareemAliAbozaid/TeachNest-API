namespace TechNest.Domain.DTOs.CategoriesDto
{
    public record UpdateCategoryDto(
        int Id,
        string Name,
        string Description
    );

}
