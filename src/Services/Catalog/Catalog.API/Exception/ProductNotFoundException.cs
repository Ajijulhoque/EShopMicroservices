namespace Catalog.API.Exception
{
    public class ProductNotFoundException(Guid id) : System.Exception($"Product not found by Id:{id}!");
}
