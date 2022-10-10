using FSH.WebApi.Application.Catalog.JobTypeCategories;
using FSH.WebApi.Application.Catalog.JobTypes;

namespace FSH.WebApi.Host.Controllers.Catalog;

public class JobTypeCategoriesController : VersionedApiController
{
    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.JobTypeCategories)]
    [OpenApiOperation("Search job type categories using available filters.", "")]
    public Task<PaginationResponse<JobTypeCategoryDto>> SearchAsync(SearchJobTypeCategoriesRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.JobTypeCategories)]
    [OpenApiOperation("Get job type category details.", "")]
    public Task<JobTypeCategoryDto> GetAsync(Guid id)
    {
        return Mediator.Send(new GetJobTypeCategoryRequest(id));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.JobTypeCategories)]
    [OpenApiOperation("Create a new job type category.", "")]
    public Task<Guid> CreateAsync(CreateJobTypeCategoryRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.JobTypeCategories)]
    [OpenApiOperation("Update a job type category.", "")]
    public async Task<ActionResult<Guid>> UpdateAsync(UpdateJobTypeCategoryRequest request, Guid id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.JobTypeCategories)]
    [OpenApiOperation("Delete a job type category.", "")]
    public Task<Guid> DeleteAsync(Guid id)
    {
        return Mediator.Send(new DeleteJobTypeCategoryRequest(id));
    }
}