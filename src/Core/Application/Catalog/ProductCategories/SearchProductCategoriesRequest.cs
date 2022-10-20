namespace FSH.WebApi.Application.Catalog.ProductCategories;

public class SearchProductCategoriesRequest : PaginationFilter, IRequest<PaginationResponse<ProductCategoryDto>>
{
}

public class ProductCategoriesBySearchRequestSpec : EntitiesByPaginationFilterSpec<ProductCategory, ProductCategoryDto>
{
    public ProductCategoriesBySearchRequestSpec(SearchProductCategoriesRequest request)
        : base(request) =>
        Query.OrderBy(c => c.Name, !request.HasOrderBy());
}

public class SearchProductCategoriesRequestHandler : IRequestHandler<SearchProductCategoriesRequest, PaginationResponse<ProductCategoryDto>>
{
    private readonly IReadRepository<ProductCategory> _repository;

    public SearchProductCategoriesRequestHandler(IReadRepository<ProductCategory> repository) => _repository = repository;

    public async Task<PaginationResponse<ProductCategoryDto>> Handle(SearchProductCategoriesRequest request, CancellationToken cancellationToken)
    {
        var spec = new ProductCategoriesBySearchRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}