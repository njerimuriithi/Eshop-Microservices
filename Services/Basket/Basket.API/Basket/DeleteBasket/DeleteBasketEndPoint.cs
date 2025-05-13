﻿
namespace Basket.API.Basket.DeleteBasket
{
    public record DeleteBasketResponse(bool IsSuccess);
    public class DeleteBasketEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/basket/{userName}", async (string userName, ISender sender) =>
            {
                var result = await sender.Send(new DeleteBasketCommand(userName));
                var response = result.Adapt<DeleteBasketResponse>();

                return Results.Ok(response);

            }).WithName("DeleteProduct")
       .Produces<DeleteBasketResponse>(StatusCodes.Status201Created)
       .Produces(StatusCodes.Status400BadRequest)
       .WithSummary("Delete Product")
       .WithDescription("Delete Product");
        }
    
    }
}
