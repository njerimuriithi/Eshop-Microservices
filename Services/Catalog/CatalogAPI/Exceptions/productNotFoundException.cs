using BuildingBlocks.Exceptions;

namespace CatalogAPI.Exceptions
{
    public class productNotFoundException:NotFoundException
    {
        public productNotFoundException(Guid id) : base("Product",id)
        {

        }
    }
}
