﻿namespace CatalogAPI.Products.GetProducts;
public record GetProductsQuery():IQuery<GetProductsResults>;

public record  GetProductsResults(IEnumerable<Product> Products);

    internal class GetProductsQueryHandler(IDocumentSession session) :IQueryHandler<GetProductsQuery,GetProductsResults>
    {
    public async Task <GetProductsResults> Handle(GetProductsQuery query, CancellationToken cancellationToken)
    {
       
        var products = await session.Query<Product>().ToListAsync(cancellationToken);
        return new GetProductsResults(products);


    }

    }

