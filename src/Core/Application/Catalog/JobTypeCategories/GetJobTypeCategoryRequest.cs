namespace FSH.WebApi.Application.Catalog.JobTypeCategories;

public class GetJobTypeCategoryRequest : IRequest<JobTypeCategoryDto>
{
    public Guid Id { get; set; }

    public GetJobTypeCategoryRequest(Guid id) => Id = id;
}

public class JobTypeCategoryByIdSpec : Specification<JobTypeCategory, JobTypeCategoryDto>, ISingleResultSpecification
{
    public JobTypeCategoryByIdSpec(Guid id) =>
        Query.Where(p => p.Id == id);
}

public class GetJobTypeCategoryRequestHandler : IRequestHandler<GetJobTypeCategoryRequest, JobTypeCategoryDto>
{
    private readonly IRepository<JobTypeCategory> _repository;
    private readonly IStringLocalizer _t;

    public GetJobTypeCategoryRequestHandler(IRepository<JobTypeCategory> repository, IStringLocalizer<GetJobTypeCategoryRequestHandler> localizer) => (_repository, _t) = (repository, localizer);

    public async Task<JobTypeCategoryDto> Handle(GetJobTypeCategoryRequest request, CancellationToken cancellationToken) =>
        await _repository.GetBySpecAsync(
            (ISpecification<JobTypeCategory, JobTypeCategoryDto>)new JobTypeCategoryByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(_t["Job Type Category {0} Not Found.", request.Id]);
}