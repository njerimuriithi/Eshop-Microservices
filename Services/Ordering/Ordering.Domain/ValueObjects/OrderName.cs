

namespace Ordering.Domain.ValueObjects
{
    public record OrderName
    {
        private const int DefaultLength = 5;
        public string Value { get; }

        public OrderName(string value) => Value = value;
        public static OrderName of(string value)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(value);
           // ArgumentOutOfRangeException.ThrowIfNotEqual(value.Length, DefaultLength);
            return new OrderName(value);
        }


    }
}
