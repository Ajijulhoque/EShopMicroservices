namespace Ordering.Application.Orders.EventHandlers
{
    public class OrderUpdatedEventHandler(ILogger<OrderCreatedEventHandler> logger) : INotificationHandler<OrderUpdatedEvent>
    {
        public Task Handle(OrderUpdatedEvent notification, CancellationToken cancellationToken)
        {
            logger.LogInformation("Order Updated Event Handled for OrderId: {OrderId}", notification.Order.Id);

            return Task.CompletedTask;
        }
    }
}
