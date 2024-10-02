namespace Catalog.API.Products.GetProductByCategory;

//public record GetProductByCategoryRequest(string Category);

public record GetProductByCategoryResponse(IEnumerable<Product> Product);

public class GetProductByCategoryEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/category/{Category}", async (string Category, ISender sender) =>
        {
            var result = await sender.Send(new GetProductByCategoryQuery(Category));

            var response = result.Adapt<GetProductByCategoryResponse>();

            return Results.Ok(response);
        })
        .WithName("GetProductByCategory")
        .Produces<GetProductByCategoryResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Get product by Category")
        .WithDescription("List the product that match with the Category in the database");
    }
}
