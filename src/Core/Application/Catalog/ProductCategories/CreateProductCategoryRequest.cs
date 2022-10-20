namespace FSH.WebApi.Application.Catalog.ProductCategories;

public class CreateProductCategoryRequest : IRequest<Guid>
{
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
}

public class CreateProductCategoryRequestValidator : CustomValidator<CreateProductCategoryRequest>
{
    public CreateProductCategoryRequestValidator(IReadRepository<ProductCategory> repository, IStringLocalizer<CreateProductCategoryRequestValidator> T) =>
        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (name, ct) => await repository.GetBySpecAsync(new ProductCategoryByNameSpec(name), ct) is null)
                .WithMessage((_, name) => T["Product Category {0} already Exists.", name]);
}

public class CreateProductCategoryRequestHandler : IRequestHandler<CreateProductCategoryRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<ProductCategory> _repository;

    public CreateProductCategoryRequestHandler(IRepositoryWithEvents<ProductCategory> repository) => _repository = repository;

    public async Task<Guid> Handle(CreateProductCategoryRequest request, CancellationToken cancellationToken)
    {
        var productCategory = new ProductCategory(request.Code, request.Name);

        await _repository.AddAsync(productCategory, cancellationToken);

        return productCategory.Id;
    }
}