namespace FSH.WebApi.Application.Catalog.Products;

public class ProductDto : IDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public decimal? Rate { get; set; }
    public string? ImagePath { get; set; }
    public Guid BrandId { get; set; }
    public string BrandName { get; set; } = default!;
    public Guid ProductCategoryId { get; set; }
    public virtual string ProductCategoryName { get; set; } = default!;
    public decimal WholesaleExTaxPrice { get; set; }
    public decimal WholesaleTaxPrice { get; set; }
    public decimal RetailExTaxPrice { get; set; }
    public decimal RetailTaxPrice { get; set; }
    public string? Unit { get; set; }
}