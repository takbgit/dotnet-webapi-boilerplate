using FSH.WebApi.Application.Catalog.ProductCategories;

namespace FSH.WebApi.Host.Controllers.Catalog;

public class ProductCategoriesController : VersionedApiController
{
    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.ProductCategories)]
    [OpenApiOperation("Search product categories using available filters.", "")]
    public Task<PaginationResponse<ProductCategoryDto>> SearchAsync(SearchProductCategoriesRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.ProductCategories)]
    [OpenApiOperation("Get product category details.", "")]
    public Task<ProductCategoryDto> GetAsync(Guid id)
    {
        return Mediator.Send(new GetProductCategoryRequest(id));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.ProductCategories)]
    [OpenApiOperation("Create a new product category.", "")]
    public Task<Guid> CreateAsync(CreateProductCategoryRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.ProductCategories)]
    [OpenApiOperation("Update a product category.", "")]
    public async Task<ActionResult<Guid>> UpdateAsync(UpdateProductCategoryRequest request, Guid id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.ProductCategories)]
    [OpenApiOperation("Delete a product category.", "")]
    public Task<Guid> DeleteAsync(Guid id)
    {
        return Mediator.Send(new DeleteProductCategoryRequest(id));
    }
}