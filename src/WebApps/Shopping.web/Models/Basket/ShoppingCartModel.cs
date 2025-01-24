namespace Shopping.web.Models.Basket
{
    public class ShoppingCartModel
    {
        public string UserName { get; set; }
        public List<ShoppingCartItemModel> Items { get; set; } = new List<ShoppingCartItemModel>();
        public decimal TotalPrice => Items.Sum(x => x.Price * x.Quantity);
    }

    public class ShoppingCartItemModel
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Color { get; set; }
    }

    public record GetBasketResponse(ShoppingCartModel Cart);

    public record StoreBasketRequest(ShoppingCartModel Cart);

    public record StoreBasketResponse(string UserName);

    public record DeleteBasketResponse(bool IsSuccess);
}
