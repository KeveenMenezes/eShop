using BuildingBlocks.CQRS;
using Catalog.API.Models;

namespace Catalog.API.Products.CreateProduct;

public record CreateProductCommand(
    string name,
    string description,
    string imageFile,
    decimal price,
    List<string> categories) : ICommand<CreateProductResult>;

public record CreateProductResult(Guid Id);

internal class CreatProductCommandHandler(IDocumentSession session)
    : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(
        CreateProductCommand command, CancellationToken cancellationToken)
    {
        var product = new Product
        (
            Guid.NewGuid(),
            command.name,
            command.description,
            command.imageFile,
            command.price,
            command.categories
        );

        session.Store(product);
        await session.SaveChangesAsync(cancellationToken);

        return new CreateProductResult(product.Id);
    }
}
