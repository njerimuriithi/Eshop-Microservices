
using Ordering.Application.Orders.Commands.DeleteOrder;

namespace Ordering.API.EndPoints
{
    public record DeleteOrderResponse(bool isSuccess);
    public class DeleteOrder : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/orders/{id}",async(Guid Id, ISender sender) =>
            {
                var result = await sender.Send(new DeleteOrderCommand(Id));
                var response = result.Adapt<DeleteOrderResponse>(); 
                return Results.Ok(response);

            }).WithName("DeleteOrder")
            .Produces<DeleteOrderResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Delete Order")
            .WithDescription("DeleteOrder");
        }
    }
}
