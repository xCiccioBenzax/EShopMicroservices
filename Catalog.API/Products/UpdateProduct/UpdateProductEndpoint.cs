namespace Catalog.API.Products.UpdateProduct;

public record UpdateProductRequest(Guid id, string Name, List<string> Category, string Description, string ImageFile, decimal Price);

public record UpdateProductResponse(bool response);

public class UpdateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/products/{Id}", async (UpdateProductRequest request, ISender sender) =>
        {
            var command = request.Adapt<UpdateProductCommand>();

            var result = await sender.Send(command);

            var response = result.Adapt<UpdateProductResponse>();

            return Results.Created($"products/{request.id}", response);
        })
        .WithName("UpdateProduct")
        .Produces<UpdateProductResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Update product")
        .WithDescription("Update product");
    }
}
