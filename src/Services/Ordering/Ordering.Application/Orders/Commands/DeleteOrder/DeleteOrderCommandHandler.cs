namespace Ordering.Application.Orders.Commands.DeleteOrder
{
    public class DeleteOrderCommandHandler(IApplicationDbContext dbContext) : ICommandHandler<DeleteOrderCommand, DeleteOrderCommandResult>
    {
        public async Task<DeleteOrderCommandResult> Handle(DeleteOrderCommand command, CancellationToken cancellationToken)
        {
            var orderId = OrderId.Of(command.Id);
            var order = await dbContext.Orders.FindAsync([orderId], cancellationToken).ConfigureAwait(false);

            if(order is null)
            {
                throw new OrderNotFoundException(command.Id);
            }

            dbContext.Orders.Remove(order);

            await dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return new DeleteOrderCommandResult(true);
        }
    }
}
