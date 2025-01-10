namespace Ordering.Domain.Models.ValueObjects
{
    public record OrderItemId()
    {
        public Guid Value { get; }

        private OrderItemId(Guid value) : this() => Value = value;

        public static OrderItemId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);
            if (value == Guid.Empty)
            {
                throw new DomainException("Order item id cannot be empty");
            }
            return new OrderItemId(value);
        }
    }
}
