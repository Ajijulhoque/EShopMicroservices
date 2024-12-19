namespace Catalog.API.Products.GetProductByCategory
{
    //Create a query model 
    public record GetProductByCategoryQuery(string Category) : IQuery<GetProductByCategoryResult>;

    //Create a result model
    public record GetProductByCategoryResult(IEnumerable<Product> Products);

    //Implement handler
    internal class GetProductByCategoryQueryHandler(IDocumentSession session, ILogger<GetProductByCategoryQueryHandler> logger) : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
    {
        public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
        {
            logger.LogInformation("GetProductByCategoryHandler.Handler called with {@Query}", query);

            var products = 
                await session.Query<Product>().Where(p => p.Category.Contains(query.Category))
                .ToListAsync(cancellationToken).ConfigureAwait(false);

            return new GetProductByCategoryResult(products);
        }
    }
}
