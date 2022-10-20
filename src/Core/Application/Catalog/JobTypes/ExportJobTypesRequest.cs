using FSH.WebApi.Application.Common.Exporters;

namespace FSH.WebApi.Application.Catalog.JobTypes;

public class ExportJobTypesRequest : BaseFilter, IRequest<Stream>
{
    public string Name { get; private set; }
    public string? Code { get; private set; }
    public string? Description { get; private set; }
    public Guid JobTypeCategoryId { get; private set; }
    public virtual JobTypeCategory JobTypeCategory { get; private set; } = default!;
    public IReadOnlyCollection<Product> Products { get; set; } = new List<Product>();
}

public class ExportJobTypesRequestHandler : IRequestHandler<ExportJobTypesRequest, Stream>
{
    private readonly IReadRepository<JobType> _repository;
    private readonly IExcelWriter _excelWriter;

    public ExportJobTypesRequestHandler(IReadRepository<JobType> repository, IExcelWriter excelWriter)
    {
        _repository = repository;
        _excelWriter = excelWriter;
    }

    public async Task<Stream> Handle(ExportJobTypesRequest request, CancellationToken cancellationToken)
    {
        var spec = new ExportJobTypesWithBrandsSpecification(request);

        var list = await _repository.ListAsync(spec, cancellationToken);

        return _excelWriter.WriteToStream(list);
    }
}

public class ExportJobTypesWithBrandsSpecification : EntitiesByBaseFilterSpec<JobType, JobTypeExportDto>
{
    public ExportJobTypesWithBrandsSpecification(ExportJobTypesRequest request)
        : base(request) =>
        Query
            .Include(jt => jt.Code)
//.Where(jt => jt.Code.Equals(request.BrandId!.Value), request.BrandId.HasValue)
//            .Where(jt => jt.Rate >= request.MinimumRate!.Value, request.MinimumRate.HasValue)
//            .Where(jt => jt.Rate <= request.MaximumRate!.Value, request.MaximumRate.HasValue)'
;
}