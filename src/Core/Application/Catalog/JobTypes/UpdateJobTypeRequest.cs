namespace FSH.WebApi.Application.Catalog.JobTypes;

public class UpdateJobTypeRequest : IRequest<DefaultIdType>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Code { get; set; }
    public string? Description { get; set; }
    public Guid JobTypeCategoryId { get; set; }
    public virtual JobTypeCategory JobTypeCategory { get; set; } = default!;
    public IReadOnlyCollection<Product> Products { get; set; } = new List<Product>();
}

public class UpdateJobTypeRequestValidator : CustomValidator<UpdateJobTypeRequest>
{
    public UpdateJobTypeRequestValidator(IRepository<JobType> repository, IStringLocalizer<UpdateJobTypeRequestValidator> T) =>
        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (brand, name, ct) =>
                    await repository.GetBySpecAsync(new JobTypeByNameSpec(name), ct)
                        is not JobType existingJobType || existingJobType.Id == brand.Id)
                .WithMessage((_, name) => T["Job Type {0} already Exists.", name]);
}

public class UpdateJobTypeRequestHandler : IRequestHandler<UpdateJobTypeRequest, DefaultIdType>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<JobType> _repository;
    private readonly IStringLocalizer _t;

    public UpdateJobTypeRequestHandler(IRepositoryWithEvents<JobType> repository, IStringLocalizer<UpdateJobTypeRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<DefaultIdType> Handle(UpdateJobTypeRequest request, CancellationToken cancellationToken)
    {
        var jobType = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = jobType ?? throw new NotFoundException(_t["Job Type {0} Not Found.", request.Id]);

        jobType.Update(request.Name, request.Code, request.Description, request.JobTypeCategoryId);

        await _repository.UpdateAsync(jobType, cancellationToken);

        return request.Id;
    }
}