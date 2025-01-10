namespace Ordering.Domain.Models.ValueObjects
{
    public record OrderName()
    {
        private const int DefaultLength = 5;
        public string Value { get; }
        private OrderName(string value) : this() => Value = value;
        public static OrderName Of(string value)
        {
            ArgumentException.ThrowIfNullOrEmpty(value);
            ArgumentOutOfRangeException.ThrowIfNotEqual(value.Length, DefaultLength);
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new DomainException("Order name cannot be empty");
            }
            return new OrderName(value);
        }
    }
}
