namespace Catalog.API.Exception
{
    public class ProductNotFoundException(Guid id) : NotFoundException("Product", id);
}
