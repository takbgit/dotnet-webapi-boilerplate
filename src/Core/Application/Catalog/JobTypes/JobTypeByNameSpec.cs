namespace FSH.WebApi.Application.Catalog.JobTypes;

public class JobTypeByNameSpec : Specification<JobType>, ISingleResultSpecification
{
    public JobTypeByNameSpec(string name) =>
        Query.Where(p => p.Name == name);
}