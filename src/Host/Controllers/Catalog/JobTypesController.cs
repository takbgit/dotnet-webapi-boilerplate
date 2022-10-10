using FSH.WebApi.Application.Catalog.JobTypes;

namespace FSH.WebApi.Host.Controllers.Catalog;

public class JobTypesController : VersionedApiController
{
    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.JobTypes)]
    [OpenApiOperation("Search job types using available filters.", "")]
    public Task<PaginationResponse<JobTypeDto>> SearchAsync(SearchJobTypesRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.JobTypes)]
    [OpenApiOperation("Get job type details.", "")]
    public Task<JobTypeDto> GetAsync(Guid id)
    {
        return Mediator.Send(new GetJobTypeRequest(id));
    }

    [HttpGet("dapper")]
    [MustHavePermission(FSHAction.View, FSHResource.JobTypes)]
    [OpenApiOperation("Get job type details via dapper.", "")]
    public Task<JobTypeDto> GetDapperAsync(Guid id)
    {
        return Mediator.Send(new GetJobTypeViaDapperRequest(id));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.JobTypes)]
    [OpenApiOperation("Create a new job type.", "")]
    public Task<Guid> CreateAsync(CreateJobTypeRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.JobTypes)]
    [OpenApiOperation("Update a job type.", "")]
    public async Task<ActionResult<Guid>> UpdateAsync(UpdateJobTypeRequest request, Guid id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.JobTypes)]
    [OpenApiOperation("Delete a job type.", "")]
    public Task<Guid> DeleteAsync(Guid id)
    {
        return Mediator.Send(new DeleteJobTypeRequest(id));
    }

    [HttpPost("export")]
    [MustHavePermission(FSHAction.Export, FSHResource.JobTypes)]
    [OpenApiOperation("Export a job types.", "")]
    public async Task<FileResult> ExportAsync(ExportJobTypesRequest filter)
    {
        var result = await Mediator.Send(filter);
        return File(result, "application/octet-stream", "JobTypeExports");
    }
    }