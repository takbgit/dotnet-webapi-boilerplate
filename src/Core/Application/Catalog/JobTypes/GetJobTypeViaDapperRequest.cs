using Mapster;

namespace FSH.WebApi.Application.Catalog.JobTypes;

public class GetJobTypeViaDapperRequest : IRequest<JobTypeDto>
{
    public Guid Id { get; set; }

    public GetJobTypeViaDapperRequest(Guid id) => Id = id;
}

public class GetJobTypeViaDapperRequestHandler : IRequestHandler<GetJobTypeViaDapperRequest, JobTypeDto>
{
    private readonly IDapperRepository _repository;
    private readonly IStringLocalizer _t;

    public GetJobTypeViaDapperRequestHandler(IDapperRepository repository, IStringLocalizer<GetJobTypeViaDapperRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<JobTypeDto> Handle(GetJobTypeViaDapperRequest request, CancellationToken cancellationToken)
    {
        var product = await _repository.QueryFirstOrDefaultAsync<JobType>(
            $"SELECT * FROM public.\"JobTypes\" WHERE \"Id\"  = '{request.Id}' AND \"Tenant\" = '@tenant'", cancellationToken: cancellationToken);

        _ = product ?? throw new NotFoundException(_t["Job Type {0} Not Found.", request.Id]);

        return product.Adapt<JobTypeDto>();
    }
}