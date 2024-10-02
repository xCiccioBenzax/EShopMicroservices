namespace Catalog.API.Products.UpdateProduct;


public record UpdateProductCommand(Guid Id, string Name, List<string> Category, string Description, string ImageFile, decimal Price) : ICommand<UpdateProductResult>;
public record UpdateProductResult(bool response);

internal class UpdateProductCommandHandler(IDocumentSession session) : ICommandHandler<UpdateProductCommand, UpdateProductResult>
{
    public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        var dbProduct = await session.LoadAsync<Product>(command.Id, cancellationToken);

        if (dbProduct is null)
        {
            throw new ProductNotFoundException(command.Id);
        }

        dbProduct.Name = command.Name;
        dbProduct.Category = command.Category;
        dbProduct.Description = command.Description;
        dbProduct.ImageFile = command.ImageFile;
        dbProduct.Price = command.Price;

        session.Update(dbProduct);
        await session.SaveChangesAsync(cancellationToken);

        return new UpdateProductResult(true);
    }
}
