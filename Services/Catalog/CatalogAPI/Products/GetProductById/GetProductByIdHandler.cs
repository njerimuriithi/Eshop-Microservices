﻿

namespace CatalogAPI.Products.GetProductById
{
    public record GetProductByIdQuery(Guid id):IQuery<GetProductByIdResult>;
    public record GetProductByIdResult(Product Product);
    internal class GetProductByIdQueryHandler(IDocumentSession session) : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
    {
        public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        {
           
             var product = await session.LoadAsync<Product>(query.id, cancellationToken); 
            if(product is  null) { throw new productNotFoundException(query.id);}
              
            return new GetProductByIdResult(product);
        }
    }
}
