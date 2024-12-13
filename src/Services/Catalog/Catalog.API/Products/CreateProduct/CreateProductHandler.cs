﻿

namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductCommand(Guid Id, string Name, List<string> Category, string Description, string ImageFile, decimal Price) : ICommand<CreateProductResult>;

    public record CreateProductResult(Guid Id);

    internal class CreateProductCommandHandler(IDocumentSession session) : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            // Create product entity from command object
            var product = new Product
            {
                Name = command.Name,
                Category = command.Category,
                Description = command.Description,
                ImageFile = command.ImageFile,
                Price = command.Price
            };
            // Save product entity to database
            session.Store(product);
            await session.SaveChangesAsync(cancellationToken);
            
            // Return CreateProductResult object
            return new CreateProductResult(product.Id);
        }
    }
}
