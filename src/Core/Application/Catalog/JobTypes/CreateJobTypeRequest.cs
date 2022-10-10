using FSH.WebApi.Domain.Common.Events;

namespace FSH.WebApi.Application.Catalog.JobTypes;

public class CreateJobTypeRequest : IRequest<Guid>
{
    public string Name { get; set; } = default!;
    public string Code { get; set; } = default!;
    public string? Description { get; set; }
    public Guid JobTypeCategoryId { get; set; }
}

public class CreateJobTypeRequestHandler : IRequestHandler<CreateJobTypeRequest, Guid>
{
    private readonly IRepository<JobType> _repository;

    public CreateJobTypeRequestHandler(IRepository<JobType> repository)
    {
        if (repository != null) (_repository) = (repository);
    }

    public async Task<Guid> Handle(CreateJobTypeRequest request, CancellationToken cancellationToken)
    {
        var jobType = new JobType(request.Name, request.Code, request.Description, request.JobTypeCategoryId);

        // Add Domain Events to be raised after the commit
        jobType.DomainEvents.Add(EntityCreatedEvent.WithEntity(jobType));

        await _repository.AddAsync(jobType, cancellationToken);

        return jobType.Id;
    }
}