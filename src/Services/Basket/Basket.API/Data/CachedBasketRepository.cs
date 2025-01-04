namespace Basket.API.Data
{
    public class CachedBasketRepository(IBasketRepository repository, IDistributedCache cache) : IBasketRepository
    {
        public async Task<ShoppingCart> GetBasket(string userName, CancellationToken cancellationToken = default)
        {
            var cachedBasket  = await cache.GetStringAsync(userName, cancellationToken).ConfigureAwait(false);
            if (!string.IsNullOrEmpty(cachedBasket))
            {
                return JsonSerializer.Deserialize<ShoppingCart>(cachedBasket)!;
            }
            
            var basket = await repository.GetBasket(userName, cancellationToken).ConfigureAwait(false);

            await cache.SetStringAsync(userName, JsonSerializer.Serialize(basket), cancellationToken).ConfigureAwait(false);

            return basket;
        }

        public async Task<ShoppingCart> StoreBasket(ShoppingCart basket, CancellationToken cancellationToken = default)
        {
            await repository.StoreBasket(basket, cancellationToken).ConfigureAwait(false);
            await cache.SetStringAsync(basket.UserName, JsonSerializer.Serialize(basket), cancellationToken).ConfigureAwait(false);
            return basket;
        }

        public async Task<bool> DeleteBasket(string userName, CancellationToken cancellationToken = default)
        {
            await repository.DeleteBasket(userName, cancellationToken).ConfigureAwait(false);
            await cache.RemoveAsync(userName, cancellationToken).ConfigureAwait(false);
            return true;
        }
    }
}
