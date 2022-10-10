namespace FSH.WebApi.Application.Catalog.JobTypes;

public class JobTypeDto : IDto
{
    public Guid Id { get; set; }
    public string? Name { get; set; } = default!;
    public string? Code { get; set; }
    public string? Description { get; set; }
    public int JobTypeCategoryId { get; set; }
    public string JobTypeCategoryName { get; set; } = default!;
    public IReadOnlyCollection<Product> Products { get; set; } = new List<Product>();

}