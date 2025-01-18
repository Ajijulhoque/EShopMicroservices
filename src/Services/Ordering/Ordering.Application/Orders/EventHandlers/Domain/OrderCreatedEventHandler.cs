namespace Ordering.Application.Orders.EventHandlers.Domain
{
    public class OrderCreatedEventHandler(IPublishEndpoint publishEndpoint, IFeatureManager featureManager, ILogger<OrderCreatedEvent> logger) : INotificationHandler<OrderCreatedEvent>
    {
        public async Task Handle(OrderCreatedEvent domainEvent, CancellationToken cancellationToken)
        {
            logger.LogInformation("Order {OrderId} is successfully created", domainEvent.Order.Id);

            if(await featureManager.IsEnabledAsync("OrderFullfilment"))
            {
                var orderCreatedIntegrationEvent = domainEvent.Order.ToOrderDto();

                await publishEndpoint.Publish(orderCreatedIntegrationEvent, cancellationToken);
            }         
        }
    }
}
