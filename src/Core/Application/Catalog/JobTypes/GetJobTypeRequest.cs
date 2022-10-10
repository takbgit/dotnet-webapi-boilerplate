namespace FSH.WebApi.Application.Catalog.JobTypes;

public class GetJobTypeRequest : IRequest<JobTypeDto>
{
    public Guid Id { get; set; }

    public GetJobTypeRequest(Guid id) => Id = id;
}

public class GetJobTypeRequestHandler : IRequestHandler<GetJobTypeRequest, JobTypeDto>
{
    private readonly IRepository<JobType> _repository;
    private readonly IStringLocalizer _t;

    public GetJobTypeRequestHandler(IRepository<JobType> repository, IStringLocalizer<GetJobTypeRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<JobTypeDto> Handle(GetJobTypeRequest request, CancellationToken cancellationToken) =>
        await _repository.GetBySpecAsync(
            (ISpecification<JobType, JobTypeDto>)new JobTypeByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(_t["JobType {0} Not Found.", request.Id]);
}