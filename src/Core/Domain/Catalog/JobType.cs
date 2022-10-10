namespace FSH.WebApi.Domain.Catalog;

public class JobType : AuditableEntity, IAggregateRoot
{
    public string Name { get; private set; }
    public string? Code { get; private set; }
    public string? Description { get; private set; }
    public Guid JobTypeCategoryId { get; private set; }
    public virtual JobTypeCategory JobTypeCategory { get; private set; } = default!;
    public IReadOnlyCollection<Product> Products { get; set; } = new List<Product>();

    public JobType(string name, string? code, string? description, Guid jobTypeCategoryId)
    {
        Name = name;
        Description = description;
        JobTypeCategoryId = jobTypeCategoryId;
    }

    public JobType Update(string? name, string? code, string? description, Guid? jobTypeCategoryId)
    {
        if (name is not null && Name?.Equals(name) is not true) Name = name;
        if (code is not null && Code?.Equals(code) is not true) Code = code;
        if (description is not null && Description?.Equals(description) is not true) Description = description;
        if (jobTypeCategoryId.HasValue && jobTypeCategoryId.Value != Guid.Empty && !JobTypeCategoryId.Equals(jobTypeCategoryId.Value)) JobTypeCategoryId = jobTypeCategoryId.Value;
        return this;
    }
}