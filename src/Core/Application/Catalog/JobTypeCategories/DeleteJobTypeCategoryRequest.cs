namespace FSH.WebApi.Application.Catalog.JobTypeCategories;

public class DeleteJobTypeCategoryRequest : IRequest<Guid>
{
    public Guid Id { get; set; }

    public DeleteJobTypeCategoryRequest(Guid id) => Id = id;
}

public class DeleteJobTypeCategoryRequestHandler : IRequestHandler<DeleteJobTypeCategoryRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<JobTypeCategory> _brandRepo;
    private readonly IReadRepository<Product> _productRepo;
    private readonly IStringLocalizer _t;

    public DeleteJobTypeCategoryRequestHandler(IRepositoryWithEvents<JobTypeCategory> brandRepo, IReadRepository<Product> productRepo, IStringLocalizer<DeleteJobTypeCategoryRequestHandler> localizer) =>
        (_brandRepo, _productRepo, _t) = (brandRepo, productRepo, localizer);

    public async Task<Guid> Handle(DeleteJobTypeCategoryRequest request, CancellationToken cancellationToken)
    {
        //if (await _productRepo.AnyAsync(new ProductsByJobTypeCategorySpec(request.Id), cancellationToken))
        //{
        //    throw new ConflictException(_t["JobTypeCategory cannot be deleted as it's being used."]);
        //}

        var brand = await _brandRepo.GetByIdAsync(request.Id, cancellationToken);

        _ = brand ?? throw new NotFoundException(_t["Job Type Category {0} Not Found."]);

        await _brandRepo.DeleteAsync(brand, cancellationToken);

        return request.Id;
    }
}