using System.Net.Cache;
using FSH.WebApi.Domain.Common.Events;

namespace FSH.WebApi.Application.Catalog.ProductCategories;

public class UpdateProductCategoryRequest : IRequest<Guid>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string Code { get; set; } = default!;
}

public class UpdateProductCategoryRequestHandler : IRequestHandler<UpdateProductCategoryRequest, Guid>
{
    private readonly IRepository<ProductCategory> _repository;
    private readonly IStringLocalizer _t;

    public UpdateProductCategoryRequestHandler(IRepository<ProductCategory> repository, IStringLocalizer<UpdateProductCategoryRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<Guid> Handle(UpdateProductCategoryRequest request, CancellationToken cancellationToken)
    {
        var productCategory = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = productCategory ?? throw new NotFoundException(_t["Product Category {0} Not Found.", request.Id]);

        var updatedProductCategory = productCategory.Update(request.Name);

        // Add Domain Events to be raised after the commit
        productCategory.DomainEvents.Add(EntityUpdatedEvent.WithEntity(productCategory));

        await _repository.UpdateAsync(updatedProductCategory, cancellationToken);

        return request.Id;
    }
}