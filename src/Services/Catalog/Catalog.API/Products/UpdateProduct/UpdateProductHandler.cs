
namespace Catalog.API.Products.UpdateProduct;

public record UpdateProductCommand(
    Guid Id,
    string Name,
    string Description,
    string ImageFile,
    decimal Price,
    List<string> Categories) : ICommand<UpdateProductResult>;

public record UpdateProductResult(Guid Id);

public class UpdateProductCommandHandler
    (IDocumentSession session, ILogger<UpdateProductCommandHandler> logger)
    : ICommandHandler<UpdateProductCommand, UpdateProductResult>
{
    public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("Updating product {Id}", command.Id);

        var product = await session.LoadAsync<Product>(
            command.Id, cancellationToken) ??
                throw new ProductNotFoundException(command.Id);

        product.Update(
            command.Name,
            command.Description,
            command.ImageFile, 
            command.Price, 
            command.Categories);

        session.Update(product);
        await session.SaveChangesAsync(cancellationToken);

        return new UpdateProductResult(product.Id);

    }
}
