namespace FSH.WebApi.Application.Catalog.JobTypes;

public class DeleteJobTypeRequest : IRequest<Guid>
{
    public Guid Id { get; set; }

    public DeleteJobTypeRequest(Guid id) => Id = id;
}

public class DeleteJobTypeRequestHandler : IRequestHandler<DeleteJobTypeRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<JobType> _brandRepo;
    private readonly IReadRepository<Product> _productRepo;
    private readonly IStringLocalizer _t;

    public DeleteJobTypeRequestHandler(IRepositoryWithEvents<JobType> brandRepo, IReadRepository<Product> productRepo, IStringLocalizer<DeleteJobTypeRequestHandler> localizer) =>
        (_brandRepo, _productRepo, _t) = (brandRepo, productRepo, localizer);

    public async Task<Guid> Handle(DeleteJobTypeRequest request, CancellationToken cancellationToken)
    {
        //if (await _productRepo.AnyAsync(new ProductsByJobTypeSpec(request.Id), cancellationToken))
        //{
        //    throw new ConflictException(_t["Job Type cannot be deleted as it's being used."]);
        //}

        var brand = await _brandRepo.GetByIdAsync(request.Id, cancellationToken);

        _ = brand ?? throw new NotFoundException(_t["Job Type {0} Not Found."]);

        await _brandRepo.DeleteAsync(brand, cancellationToken);

        return request.Id;
    }
}