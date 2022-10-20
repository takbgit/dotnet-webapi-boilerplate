namespace FSH.WebApi.Application.Catalog.ProductCategories;

public class ProductCategoryByNameSpec : Specification<ProductCategory>, ISingleResultSpecification
{
    public ProductCategoryByNameSpec(string name) =>
        Query.Where(b => b.Name == name);
}