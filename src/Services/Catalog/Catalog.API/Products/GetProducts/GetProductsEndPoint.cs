namespace Catalog.API.Products.GetProducts
{
    //public record GetProductsRequest();
    public record GetProductsResponse(IEnumerable<Product> Products);

    public class GetProductsEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products", async (ISender sender) =>
            {
                var query = new GetProductsQuery();
                var result = await sender.Send(query).ConfigureAwait(false);
                var response = new GetProductsResponse(result.Products);
                return Results.Ok(response);
            })
                .WithName("GetProducts")
                .Produces<GetProductsResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Get Products")
                .WithDescription("Get all products");
        }
    }
}
