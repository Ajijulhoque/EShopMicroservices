namespace Catalog.API.Products.GetProducts
{
    public record GetProductsQuery(int? PageNumber, int? PageSize) : IQuery<GetProductsResult>;
    public record GetProductsResult(IEnumerable<Product> Products);

    internal class GetProductsHandler(IDocumentSession session) : IQueryHandler<GetProductsQuery, GetProductsResult>
    {
        public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
        {
            var pageNumber = query.PageNumber ?? 1;
            var pageSize = query.PageSize ?? 10;
            var products = await session.Query<Product>()
                .ToPagedListAsync(pageNumber, pageSize, cancellationToken).ConfigureAwait(false);

            return new GetProductsResult(products);
        }
    }
}
