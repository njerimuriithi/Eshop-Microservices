

namespace Ordering.Domain.ValueObjects
{
    public record CustomerId
    {
        public Guid Value { get; }
        private  CustomerId(Guid value) => Value = value;

        public static CustomerId of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);    
            if(value == Guid.Empty)
            {
                throw new DomainException("CustomerId Cannot Be Empty");

            }
            return new CustomerId(value);
        }
           


    }
}
