namespace FSH.WebApi.Application.Catalog.JobTypes;

public class JobTypeExportDto : IDto
{
    public string? Name { get; set; } = default!;
    public string? Code { get; set; }
    public string? Description { get; set; }
    public string JobTypeCategoryName { get; set; } = default!;

}