using Discount.Grpc;

using NetTopologySuite.Index.HPRtree;

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
    
    public class StoreBasketCommandHandler(IBasketRepository repository, DiscountProtoService.DiscountProtoServiceClient discountProto) : ICommandHandler<StoreBasketCommand,StoreBasketResult>
    {
        public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
        {
            var cart = command.Cart;
            await ApplyDiscount(cart, cancellationToken).ConfigureAwait(false);

            //Store data in DB
            await repository.StoreBasket(cart, cancellationToken).ConfigureAwait(false);

            //Update Cache

            return new StoreBasketResult(cart.UserName);
        }

        private async Task ApplyDiscount(ShoppingCart cart, CancellationToken cancellationToken)
        {
            foreach (var cartItem in cart.Items)
            {
                var coupon = await discountProto.GetDiscountAsync(new GetDiscountRequest { ProductName = cartItem.ProductName }, cancellationToken:cancellationToken).ConfigureAwait(false);
                cartItem.Price -= coupon.Amount;
            }
        }
    }
}
