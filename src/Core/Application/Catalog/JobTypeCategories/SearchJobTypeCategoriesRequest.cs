namespace FSH.WebApi.Application.Catalog.JobTypeCategories;

public class SearchJobTypeCategoriesRequest : PaginationFilter, IRequest<PaginationResponse<JobTypeCategoryDto>>
{
}

public class JobTypeCategoriesBySearchRequestSpec : EntitiesByPaginationFilterSpec<JobTypeCategory, JobTypeCategoryDto>
{
    public JobTypeCategoriesBySearchRequestSpec(SearchJobTypeCategoriesRequest request)
        : base(request) =>
        Query.OrderBy(c => c.Name, !request.HasOrderBy());
}

public class SearchJobTypeCategoriesRequestHandler : IRequestHandler<SearchJobTypeCategoriesRequest, PaginationResponse<JobTypeCategoryDto>>
{
    private readonly IReadRepository<JobTypeCategory> _repository;

    public SearchJobTypeCategoriesRequestHandler(IReadRepository<JobTypeCategory> repository) => _repository = repository;

    public async Task<PaginationResponse<JobTypeCategoryDto>> Handle(SearchJobTypeCategoriesRequest request, CancellationToken cancellationToken)
    {
        var spec = new JobTypeCategoriesBySearchRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}