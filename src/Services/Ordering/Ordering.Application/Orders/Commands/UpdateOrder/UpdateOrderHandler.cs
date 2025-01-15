namespace Ordering.Application.Orders.Commands.UpdateOrder
{
    public class UpdateOrderHandler(IApplicationDbContext dbContext) : ICommandHandler<UpdateOrderCommand, UpdateOrderResult>
    {
        public async Task<UpdateOrderResult> Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
        {
            var orderId = OrderId.Of(command.Order.Id);

            var order = await dbContext.Orders.FindAsync([orderId], cancellationToken: cancellationToken).ConfigureAwait(false);

            if (order is null)
            {
                throw new OrderNotFoundException(command.Order.Id);
            }

            UpdateOrder(order, command.Order);

            dbContext.Orders.Update(order);

            await dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return new UpdateOrderResult(true);
        }

        private static void UpdateOrder(Order order, OrderDto orderDto)
        {
            var updatedShippingAddress = Address.Of(orderDto.ShippingAddress.FirstName, orderDto.ShippingAddress.LastName, orderDto.ShippingAddress.EmailAddress, orderDto.ShippingAddress.AddressLine, orderDto.ShippingAddress.Country, orderDto.ShippingAddress.State, orderDto.ShippingAddress.ZipCode);
            var updatedBillingAddress = Address.Of(orderDto.BillingAddress.FirstName, orderDto.BillingAddress.LastName, orderDto.BillingAddress.EmailAddress, orderDto.BillingAddress.AddressLine, orderDto.BillingAddress.Country, orderDto.BillingAddress.State, orderDto.BillingAddress.ZipCode);

            var updatedPaymentMethod = Payment.Of(orderDto.Payment.CardName,orderDto.Payment.CardNumber,orderDto.Payment.Expiration, orderDto.Payment.Cvv, orderDto.Payment.PaymentMethod);

            order.Update(OrderName.Of(orderDto.OrderName), updatedShippingAddress, updatedBillingAddress, updatedPaymentMethod, orderDto.Status);
        }
    }
}
