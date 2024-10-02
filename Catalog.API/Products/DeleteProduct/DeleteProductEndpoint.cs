namespace Catalog.API.Products.DeleteProduct;

//public record DeleteProductRequest(Guid Id);

public record DeleteProductResponse(bool response);

public class DeleteProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/products/{Id}", async (Guid Id, ISender sender) =>
        {
            var result = await sender.Send(new DeleteProductCommand(Id));

            var response = result.Adapt<DeleteProductResponse>();

            return Results.Ok(response);
        })
        .WithName("UpdateProduct")
        .Produces<DeleteProductResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Update product")
        .WithDescription("Update product");
    }
}
