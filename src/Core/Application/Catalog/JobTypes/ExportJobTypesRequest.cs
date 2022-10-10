using FSH.WebApi.Application.Common.Exporters;

namespace FSH.WebApi.Application.Catalog.JobTypes;

public class ExportJobTypesRequest : BaseFilter, IRequest<Stream>
{
    public Guid? BrandId { get; set; }
    public decimal? MinimumRate { get; set; }
    public decimal? MaximumRate { get; set; }
}

public class ExportJobTypesRequestHandler : IRequestHandler<ExportJobTypesRequest, Stream>
{
    private readonly IReadRepository<Product> _repository;
    private readonly IExcelWriter _excelWriter;

    public ExportJobTypesRequestHandler(IReadRepository<Product> repository, IExcelWriter excelWriter)
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

public class ExportJobTypesWithBrandsSpecification : EntitiesByBaseFilterSpec<Product, JobTypeExportDto>
{
    public ExportJobTypesWithBrandsSpecification(ExportJobTypesRequest request)
        : base(request) =>
        Query
            .Include(p => p.Brand)
            .Where(p => p.BrandId.Equals(request.BrandId!.Value), request.BrandId.HasValue)
            .Where(p => p.Rate >= request.MinimumRate!.Value, request.MinimumRate.HasValue)
            .Where(p => p.Rate <= request.MaximumRate!.Value, request.MaximumRate.HasValue);
}