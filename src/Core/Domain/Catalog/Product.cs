namespace FSH.WebApi.Domain.Catalog;

public class Product : AuditableEntity, IAggregateRoot
{
    public string Name { get; private set; } = default!;
    public string? Description { get; private set; }
    public decimal? Rate { get; private set; }
    public string? ImagePath { get; private set; }
    public Guid BrandId { get; private set; }
    public virtual Brand Brand { get; private set; } = default!;
    public Guid ProductCategoryId { get; private set; }
    public virtual ProductCategory ProductCategory { get; private set; } = default!;
    public decimal WholesaleExTaxPrice { get; set; }
    public decimal WholesaleTaxPrice { get; set; }
    public decimal RetailExTaxPrice { get; set; }
    public decimal RetailTaxPrice { get; set; }
    public string? Unit { get; set; }
    public Product()
    {
        // Only needed for working with dapper (See GetProductViaDapperRequest)
        // If you're not using dapper it's better to remove this constructor.
    }

    public Product(string name, string? description, decimal? rate, Guid brandId, Guid productCategoryId, string? imagePath,
        decimal wholesaleExTaxPrice, decimal wholesaleTaxPrice, decimal retailExTaxPrice, decimal retailTaxPrice,
        string? unit)
    {
        Name = name;
        Description = description;
        Rate = rate;
        ImagePath = imagePath;
        BrandId = brandId;
        ProductCategoryId = productCategoryId;
        WholesaleExTaxPrice = wholesaleExTaxPrice;
        WholesaleTaxPrice = wholesaleTaxPrice;
        RetailExTaxPrice = retailExTaxPrice;
        RetailTaxPrice = retailTaxPrice;
        Unit = unit;
    }

    public Product Update(string? name, string? description, decimal? rate, Guid? brandId, Guid? productCategoryId, string? imagePath,
        decimal? wholesaleExTaxPrice, decimal? wholesaleTaxPrice, decimal? retailExTaxPrice, decimal? retailTaxPrice,
        string? unit)
    {
        if (name is not null && Name?.Equals(name) is not true) Name = name;
        if (description is not null && Description?.Equals(description) is not true) Description = description;
        if (rate.HasValue && Rate != rate) Rate = rate.Value;
        if (brandId.HasValue && brandId.Value != Guid.Empty && !BrandId.Equals(brandId.Value)) BrandId = brandId.Value;
        if (productCategoryId.HasValue && productCategoryId.Value != Guid.Empty && !ProductCategoryId.Equals(productCategoryId.Value)) ProductCategoryId = productCategoryId.Value;
        if (imagePath is not null && ImagePath?.Equals(imagePath) is not true) ImagePath = imagePath;
        if (wholesaleExTaxPrice.HasValue && WholesaleExTaxPrice != wholesaleExTaxPrice)
            WholesaleExTaxPrice = wholesaleExTaxPrice.Value;
        if (wholesaleTaxPrice.HasValue && WholesaleTaxPrice != wholesaleTaxPrice)
            WholesaleTaxPrice = wholesaleTaxPrice.Value;
        if (retailExTaxPrice.HasValue && RetailExTaxPrice != retailExTaxPrice)
            RetailExTaxPrice = retailExTaxPrice.Value;
        if (retailTaxPrice.HasValue && RetailTaxPrice != retailTaxPrice)
            RetailTaxPrice = retailTaxPrice.Value;

        return this;
    }

    public Product ClearImagePath()
    {
        ImagePath = string.Empty;
        return this;
    }
}