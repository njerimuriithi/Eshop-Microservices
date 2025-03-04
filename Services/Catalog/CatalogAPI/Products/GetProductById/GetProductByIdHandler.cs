

namespace CatalogAPI.Products.GetProductById
{
    public record GetProductByIdQuery(Guid id):IQuery<GetProductByIdResult>;
    public record GetProductByIdResult(Product Product);
    internal class GetProductByIdQueryHandler(IDocumentSession session,ILogger<GetProductByIdQueryHandler>logger) : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
    {
        public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        {
            logger.LogInformation("GetProductbyIdQueryHandler{@query}");
             var product = await session.LoadAsync<Product>(query.id, cancellationToken); 
            if(product is  null) { throw new productNotFoundException();}
              
            return new GetProductByIdResult(product);
        }
    }
}
