namespace Catalog.API.Products.GetProductById;

//public record GetProductByIdRequest(Guid Id);

public record GetProductByIdResponse(Product Product);

public class GetProductByCategoryEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/{Id}", async (Guid Id, ISender sender) =>
        {
            var result = await sender.Send(new GetProductByIdQuery(Id));

            var response = result.Adapt<GetProductByIdResponse>();

            return Results.Ok(response);
        })
        .WithName("GetProductById")
        .Produces<GetProductByIdResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Get product by Id")
        .WithDescription("List the product that match with the ID in the database");
    }
}
