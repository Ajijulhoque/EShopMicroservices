
namespace Shopping.web.Services
{
    public interface IBasketService
    {
        [Get("/basket-service/basket/{userName}")]
        Task<GetBasketResponse> GetBasket(string userName);

        [Post("/basket-service/basket")]
        Task<StoreBasketResponse> StoreBasket(StoreBasketRequest request);

        [Delete("/basket-service/basket/{userName}")]
        Task<DeleteBasketResponse> DeleteBasket(string userName);

        [Post("/basket-service/basket/checkout")]
        Task<CheckoutBasketResponse> CheckoutBasket(CheckoutBasketRequest request);

        public async Task<ShoppingCartModel> LoadUserBasket()
        {
            var userName = "swn";
            ShoppingCartModel basket;

            try
            {
                var basketResponse = await GetBasket(userName);
                basket = basketResponse.Cart;
            }
            catch (Exception)
            {
                basket = new ShoppingCartModel { UserName = userName };
            }

            return basket;
        }
    }
}
