namespace Catalog.API.Products.GetProductByCategory
{
    //public record GetProductByCategoryRequest();
    public record GetProductByCategoryResponse(IEnumerable<Product> Products);
    
    public class GetProductByCategoryEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.Map("/products/category/{category}", async (string category, ISender sender) =>
            {
                var result = await sender.Send(new GetProductByCategoryQuery(category)).ConfigureAwait(false);
                var response = result.Adapt<GetProductByCategoryResponse>();

                return Results.Ok(response);
            })
                .WithName(nameof(GetProductByCategoryEndPoint))
                .Produces<GetProductByCategoryResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .WithSummary("Get Products by Category")
                .WithDescription("Retrieve Products by its Category");
        }
    }
}
