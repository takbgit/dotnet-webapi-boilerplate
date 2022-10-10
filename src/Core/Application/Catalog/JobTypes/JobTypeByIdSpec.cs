namespace FSH.WebApi.Application.Catalog.JobTypes;

public class JobTypeByIdSpec : Specification<JobType, JobTypeDto>, ISingleResultSpecification
{
    public JobTypeByIdSpec(Guid id) =>
        Query
            .Where(p => p.Id == id);
}