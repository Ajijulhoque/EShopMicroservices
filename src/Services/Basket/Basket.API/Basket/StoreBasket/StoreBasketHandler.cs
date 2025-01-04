namespace Basket.API.Basket.StoreBasket
{
    public record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasketResult>;
    public record StoreBasketResult(string UserName);

    public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
    {
        public StoreBasketCommandValidator()
        {
            RuleFor(x => x.Cart).NotNull().WithMessage("Cart can't be Null");
            RuleFor(x => x.Cart.UserName).NotEmpty().WithMessage("User name is required");
        }
    }
    
    public class StoreBasketCommandHandler(IBasketRepository repository) : ICommandHandler<StoreBasketCommand,StoreBasketResult>
    {
        public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
        {
            var cart = command.Cart;

            //Store data in DB
            await repository.StoreBasket(cart, cancellationToken).ConfigureAwait(false);

            //Update Cache

            return new StoreBasketResult(cart.UserName);
        }
    }
}
