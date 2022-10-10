namespace FSH.WebApi.Application.Catalog.JobTypeCategories;

public class JobTypeCategoryByNameSpec : Specification<JobTypeCategory>, ISingleResultSpecification
{
    public JobTypeCategoryByNameSpec(string name) =>
        Query.Where(b => b.Name == name);
}