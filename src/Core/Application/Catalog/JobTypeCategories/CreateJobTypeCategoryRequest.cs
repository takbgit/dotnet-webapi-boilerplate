namespace FSH.WebApi.Application.Catalog.JobTypeCategories;

public class CreateJobTypeCategoryRequest : IRequest<Guid>
{
    public string Name { get; set; } = default!;
}

public class CreateJobTypeCategoryRequestValidator : CustomValidator<CreateJobTypeCategoryRequest>
{
    public CreateJobTypeCategoryRequestValidator(IReadRepository<JobTypeCategory> repository, IStringLocalizer<CreateJobTypeCategoryRequestValidator> T) =>
        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (name, ct) => await repository.GetBySpecAsync(new JobTypeCategoryByNameSpec(name), ct) is null)
                .WithMessage((_, name) => T["Job Type Category {0} already Exists.", name]);
}

public class CreateJobTypeCategoryRequestHandler : IRequestHandler<CreateJobTypeCategoryRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<JobTypeCategory> _repository;

    public CreateJobTypeCategoryRequestHandler(IRepositoryWithEvents<JobTypeCategory> repository) => _repository = repository;

    public async Task<Guid> Handle(CreateJobTypeCategoryRequest request, CancellationToken cancellationToken)
    {
        var jobTypeCategory = new JobTypeCategory(request.Name);

        await _repository.AddAsync(jobTypeCategory, cancellationToken);

        return jobTypeCategory.Id;
    }
}