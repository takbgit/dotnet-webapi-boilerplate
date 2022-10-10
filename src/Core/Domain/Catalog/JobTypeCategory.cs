namespace FSH.WebApi.Domain.Catalog;
public class JobTypeCategory : AuditableEntity, IAggregateRoot
{
    public string Name { get; private set; }

    // public string ICollection<JobType> JobTypes { get; private set; }
    public JobTypeCategory(string name)
    {
        Name = name;
    }

    public JobTypeCategory Update(string? name)
    {
        if (name is not null && Name?.Equals(name) is not true) Name = name;
        return this;
    }
}
