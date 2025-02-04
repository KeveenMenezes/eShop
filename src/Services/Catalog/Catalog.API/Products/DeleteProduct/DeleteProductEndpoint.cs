
namespace Catalog.API.Products.DeleteProduct;

public record DeleteProductResponse(bool IsDeleted);

public class DeleteProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/products/{id:Guid}",
            async (Guid id, ISender sender, CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(new DeleteProductCommand(id), cancellationToken);

            var response = result.Adapt<DeleteProductResponse>();

            return Results.Ok(response);
        })
        .WithName("DeleteProduct")
        .Produces<DeleteProductResponse>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status404NotFound)
        .WithSummary("Deletes a product")
        .WithDescription("Deletes a product by its id");
    }
}
