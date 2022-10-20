namespace FSH.WebApi.Domain.Catalog;
public class ProductCategory : AuditableEntity, IAggregateRoot
{
    public string Name { get; private set; }
    public string Code { get; private set; }
    public ICollection<Product> Products { get; } = new List<Product>();
    public ICollection<ProductCategory> ProductSubCategories { get; } = new List<ProductCategory>();
    public ProductCategory(string code, string name)
    {
        Code = code;
        Name = name;
    }

    public void AddSubcategory(ProductCategory productCategory)
    {
        ProductSubCategories.Add(productCategory);
    }

    public void AddProduct(Product product)
    {
        Products.Add(product);
    }

    public ProductCategory Update(string? name)
    {
        if (name is not null && Name?.Equals(name) is not true) Name = name;
        return this;
    }
}
