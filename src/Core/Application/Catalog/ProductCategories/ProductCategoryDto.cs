namespace FSH.WebApi.Application.Catalog.ProductCategories;

public class ProductCategoryDto : IDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string Code { get; set; } = default!;
}