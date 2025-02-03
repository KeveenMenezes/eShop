
namespace Catalog.API.Products.GetProductByCategory;

public record GetProductByCategoryQuery(string Category) :
    IQuery<GetProductByCategoryResult>;

public record GetProductByCategoryResult(IEnumerable<Product> Products);

internal class GetProductByCategoryQueryHandler
    (IDocumentSession session, ILogger<GetProductByCategoryQueryHandler> logger)
    : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
{
    public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Querying products by category id: {CategoryId}", request.Category);

        var products = await session.Query<Product>()
            .Where(p => p.Categories.Contains(request.Category))
            .ToListAsync(token: cancellationToken);

        return new GetProductByCategoryResult(products);
    }
}
