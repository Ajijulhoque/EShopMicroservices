namespace Catalog.API.Products.UpdateProduct
{
    public record UpdateProductRequest(Guid Id, string Name, List<string> Category, string Description, string ImageFile, decimal Price);
    public record UpdateProductResponse(Product Product);
    public class UpdateProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/products", async (UpdateProductRequest request, ISender sender) =>
            {
                var result = await sender.Send(new UpdateProductCommand(request.Id, request.Name, request.Category, request.Description, request.ImageFile, request.Price)).ConfigureAwait(false);
                var response = result.Adapt<UpdateProductResponse>();
                return Results.Ok(response);
            })
                .Produces<UpdateProductResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Update Product")
                .WithDescription("Update a product");
        }
    }
}
