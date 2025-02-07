namespace Catalog.API.Products.CreateProduct;

public record CreateProductCommand(
    string Name,
    string Description,
    string ImageFile,
    decimal Price,
    List<string> Categories)
    : ICommand<CreateProductResult>;

public record CreateProductResult(Guid Id);

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.ImageFile).NotEmpty().WithMessage("Image is required");
        RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
        RuleFor(x => x.Categories).NotEmpty().WithMessage("Categories is required");
    }
}

internal class CreateProductCommandHandler
    (IDocumentSession session, IValidator<CreateProductCommand> validator)
    : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var result = await validator.ValidateAsync(command, cancellationToken);
        var errors = result.Errors.Select(x => x.ErrorMessage).ToList();
        if(errors.Count != 0)
        {
            throw new ValidationException(string.Join("; ", errors));
        }

        var product = new Product(
            Guid.NewGuid(),
            command.Name,
            command.Description,
            command.ImageFile,
            command.Price,
            command.Categories
        );

        //save to database
        session.Store(product);
        await session.SaveChangesAsync(cancellationToken);

        //return result
        return new CreateProductResult(product.Id);
    }
}
