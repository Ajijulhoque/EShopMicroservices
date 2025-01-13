namespace Ordering.Application.Orders.EventHandlers
{
    public class OrderCreatedEventHandler(ILogger<OrderCreatedEvent> logger) : INotificationHandler<OrderCreatedEvent>
    {
        public Task Handle(OrderCreatedEvent notification, CancellationToken cancellationToken)
        {
            logger.LogInformation("Order {OrderId} is successfully created", notification.Order.Id);

            return Task.CompletedTask;
        }
    }
}
