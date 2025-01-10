namespace Ordering.Domain.Models.ValueObjects
{
    public record OrderId()
    {
        public Guid Value { get; }
        private OrderId( Guid value) : this() => Value = value;

        public static OrderId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);
            if (value == Guid.Empty)
            {
                throw new DomainException("Order id cannot be empty");
            }
            return new OrderId(value);
        }
    }
}
