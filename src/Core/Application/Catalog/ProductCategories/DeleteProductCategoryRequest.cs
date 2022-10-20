using FSH.WebApi.Domain.Common.Events;

namespace FSH.WebApi.Application.Catalog.ProductCategories;

public class DeleteProductCategoryRequest : IRequest<Guid>
{
    public Guid Id { get; set; }

    public DeleteProductCategoryRequest(Guid id) => Id = id;
}

public class DeleteProductCategoryRequestHandler : IRequestHandler<DeleteProductCategoryRequest, Guid>
{
    private readonly IRepository<ProductCategory> _repository;
    private readonly IStringLocalizer _t;

    public DeleteProductCategoryRequestHandler(IRepository<ProductCategory> repository, IStringLocalizer<DeleteProductCategoryRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<Guid> Handle(DeleteProductCategoryRequest request, CancellationToken cancellationToken)
    {
        var productCategory = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = productCategory ?? throw new NotFoundException(_t["Product Category {0} Not Found."]);

        // Add Domain Events to be raised after the commit
        productCategory.DomainEvents.Add(EntityDeletedEvent.WithEntity(productCategory));

        await _repository.DeleteAsync(productCategory, cancellationToken);

        return request.Id;
    }
}