using Marten.Schema;
using System.Xml.Linq;

namespace CatalogAPI.Data
{
    public class CatalogInitialData : IInitialData
    {
        public async Task Populate(IDocumentStore store, CancellationToken cancellation)
        {
          using var session  = store.LightweightSession();
            if(await session.Query<Product>().AnyAsync())
                return;

            session.Store<Product>(GetPriconfiguredProducts());
            await session.SaveChangesAsync();   
        }

        private static IEnumerable<Product> GetPriconfiguredProducts() => new List<Product>()
        {
            new Product()
            {
                Id=new Guid("01956127-977b-4648-87aa-fc25309660c3"),
                   Name="ProductD",
                  Category=new List<string>{"Smart Phone"},
                  Description="Description A",
                  ImageFile="ImageFileB",
                   Price=1000
            }                   


                
        };

    }
}
