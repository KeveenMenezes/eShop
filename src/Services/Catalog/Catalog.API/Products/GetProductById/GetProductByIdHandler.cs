namespace Catalog.API.Products.GetProductById;

public record GetProductByIdQuery(Guid Id) :
    IQuery<GetProductByIdResult>;

public record GetProductByIdResult(Product Product);

internal class GetProductByIdQueryHandler(
    IDocumentSession session,
    ILogger<GetProductByIdQueryHandler> logger)
    : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
{
    public async Task<GetProductByIdResult> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Querying for product with id {Id}", request.Id);

        var product = await session.LoadAsync<Product>(request.Id, cancellationToken) ??
            throw new ProductNotFoundException(request.Id);

        return new GetProductByIdResult(product);
    }
}
