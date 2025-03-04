namespace CatalogAPI.Exceptions
{
    public class productNotFoundException:Exception
    {
        public productNotFoundException() : base("Product not Found!")
        {

        }
    }
}
