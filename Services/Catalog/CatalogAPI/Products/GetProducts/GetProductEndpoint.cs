﻿

namespace CatalogAPI.Products.GetProducts
{
    public record  GetProductResponse(IEnumerable<Product> Products);
    public class GetProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
           app.MapGet("/products",async(ISender sender) =>
           {
               var result = await sender.Send(new GetProductsQuery());

               var response = result.Adapt<GetProductResponse>();
               return Results.Ok(response);
           }).WithName("GetProducts")
        .Produces<GetProductResponse>(StatusCodes.Status201Created)
        .Produces(StatusCodes.Status400BadRequest)
        .WithSummary("get Products")
        .WithDescription("Get Products");
        
    }
    }
}
