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

public class CreatProductCommandHandler
    : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(
        CreateProductCommand command, CancellationToken cancellationToken)
    {

        //TODO: Create product
        //TODO: Save product
        //TODO: Return product id

        var product = new Product
        (
            Guid.NewGuid(),
            command.name,
            command.description,
            command.imageFile,
            command.price,
            command.categories
        );

        return new CreateProductResult(Guid.NewGuid());

    }
}
