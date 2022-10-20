namespace FSH.WebApi.Application.Catalog.JobTypes;

public class SearchJobTypesRequest : PaginationFilter, IRequest<PaginationResponse<JobTypeDto>>
{
    public Guid Id { get; set; }
    public string? Name { get; set; } = default;
    public string? Code { get; set; }
    public string? Description { get; set; }
    public int JobTypeCategoryId { get; set; }
    public string JobTypeCategoryName { get; set; } = default!;
    public IReadOnlyCollection<Product> Products { get; set; } = new List<Product>();
}

public class JobTypesBySearchRequestSpec : EntitiesByPaginationFilterSpec<JobType, JobTypeDto>
{
    public JobTypesBySearchRequestSpec(SearchJobTypesRequest request)
        : base(request) =>
        Query.OrderBy(c => c.Name, !request.HasOrderBy());
}
public class SearchJobTypesRequestHandler : IRequestHandler<SearchJobTypesRequest, PaginationResponse<JobTypeDto>>
{
    private readonly IReadRepository<JobType> _repository;

    public SearchJobTypesRequestHandler(IReadRepository<JobType> repository) => _repository = repository;

    public async Task<PaginationResponse<JobTypeDto>> Handle(SearchJobTypesRequest request, CancellationToken cancellationToken)
    {
        var spec = new JobTypesBySearchRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}