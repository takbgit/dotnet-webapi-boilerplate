namespace FSH.WebApi.Application.Catalog.ProductCategories;

public class GetProductCategoryRequest : IRequest<ProductCategoryDto>
{
    public Guid Id { get; set; }

    public GetProductCategoryRequest(Guid id) => Id = id;
}

public class ProductCategoryByIdSpec : Specification<ProductCategory, ProductCategoryDto>, ISingleResultSpecification
{
    public ProductCategoryByIdSpec(Guid id) =>
        Query.Where(p => p.Id == id);
}

public class GetProductCategoryRequestHandler : IRequestHandler<GetProductCategoryRequest, ProductCategoryDto>
{
    private readonly IRepository<ProductCategory> _repository;
    private readonly IStringLocalizer _t;

    public GetProductCategoryRequestHandler(IRepository<ProductCategory> repository, IStringLocalizer<GetProductCategoryRequestHandler> localizer) => (_repository, _t) = (repository, localizer);

    public async Task<ProductCategoryDto> Handle(GetProductCategoryRequest request, CancellationToken cancellationToken) =>
        await _repository.GetBySpecAsync(
            (ISpecification<ProductCategory, ProductCategoryDto>)new ProductCategoryByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(_t["Product Category {0} Not Found.", request.Id]);
}