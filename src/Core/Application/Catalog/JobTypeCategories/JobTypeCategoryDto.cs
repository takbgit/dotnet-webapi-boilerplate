namespace FSH.WebApi.Application.Catalog.JobTypeCategories;

public class JobTypeCategoryDto : IDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
}