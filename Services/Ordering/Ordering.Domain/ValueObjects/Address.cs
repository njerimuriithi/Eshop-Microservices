namespace Ordering.Domain.ValueObjects
{
    public record Address
    {
        public string FirstName { get; } = default!;
        public string LastName { get; } = default!;

        public string? EmailAddress { get; } = default!;
        public string AddressLine {  get; } = default!; 
        public string Country { get; } = default!;
        public string State { get; } = default!;    
        public string ZipCode { get; } = default!;  

        protected Address()
        {

        }
        private  Address(string firstName,string lastName,string emailaddress,string addressLine,string country,string state,string zipCode)
        {
            FirstName = firstName;
            LastName = lastName;
            EmailAddress = emailaddress;
            AddressLine = addressLine;
            Country = country;
            State = state;
            ZipCode = zipCode;

                
        }
        public static Address of(string firstName, string lastName, string emailaddress, string addressLine, string country, string state, string zipCode)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(emailaddress);
            ArgumentException.ThrowIfNullOrWhiteSpace(addressLine);
            return new Address(firstName,lastName,emailaddress,addressLine,country ,state, zipCode);
        }


    } 
}
