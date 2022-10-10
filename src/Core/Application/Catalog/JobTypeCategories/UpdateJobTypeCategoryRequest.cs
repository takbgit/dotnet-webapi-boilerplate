namespace FSH.WebApi.Application.Catalog.JobTypeCategories;

public class UpdateJobTypeCategoryRequest : IRequest<Guid>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
}

public class UpdateJobTypeCategoryRequestValidator : CustomValidator<UpdateJobTypeCategoryRequest>
{
    public UpdateJobTypeCategoryRequestValidator(IRepository<JobTypeCategory> repository, IStringLocalizer<UpdateJobTypeCategoryRequestValidator> T) =>
        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (brand, name, ct) =>
                    await repository.GetBySpecAsync(new JobTypeCategoryByNameSpec(name), ct)
                        is not JobTypeCategory existingJobTypeCategory || existingJobTypeCategory.Id == brand.Id)
                .WithMessage((_, name) => T["Job Type Category {0} already Exists.", name]);
}

public class UpdateJobTypeCategoryRequestHandler : IRequestHandler<UpdateJobTypeCategoryRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<JobTypeCategory> _repository;
    private readonly IStringLocalizer _t;

    public UpdateJobTypeCategoryRequestHandler(IRepositoryWithEvents<JobTypeCategory> repository, IStringLocalizer<UpdateJobTypeCategoryRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<Guid> Handle(UpdateJobTypeCategoryRequest request, CancellationToken cancellationToken)
    {
        var jobTypeCategory = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = jobTypeCategory
        ?? throw new NotFoundException(_t["Job Type Category {0} Not Found.", request.Id]);

        jobTypeCategory.Update(request.Name);

        await _repository.UpdateAsync(jobTypeCategory, cancellationToken);

        return request.Id;
    }
}