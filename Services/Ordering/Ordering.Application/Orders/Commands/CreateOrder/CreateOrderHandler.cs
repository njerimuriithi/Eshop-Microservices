
namespace Ordering.Application.Orders.Commands.CreateOrder
{
    public class CreateOrderHandler(IApplicationDbContext dbContext) : ICommandHandler<CreateOrderCommand, CreateOrderResult>
    {
        public async Task<CreateOrderResult> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
        {
            var order = CreateNewOrder(command.Order);
            dbContext.Orders.Add(order);    
            await dbContext.SaveChangesAsync(cancellationToken);
            return new CreateOrderResult(order.Id.Value);
            
           
        }
        private Order CreateNewOrder(OrderDto orderDto) { 
        
         var shippingAddress = Address.of(orderDto.ShippingAddress.FirstName, orderDto.ShippingAddress.LastName,orderDto.ShippingAddress.EmailAddress,orderDto.ShippingAddress.AddressLine
                                            ,orderDto.ShippingAddress.EmailAddress,orderDto.ShippingAddress.State,orderDto.ShippingAddress.ZipCode);

          var BillingAddress = Address.of(orderDto.BillingAddress.FirstName,orderDto.BillingAddress.LastName, orderDto.BillingAddress.EmailAddress, orderDto.BillingAddress.State,
                    orderDto.BillingAddress.ZipCode,orderDto.BillingAddress.AddressLine,orderDto.BillingAddress.Country);

            var newOrder = Order.Create(
                id: OrderId.of(Guid.NewGuid()),
                customerId: CustomerId.of(orderDto.CustomerId),
                orderName: OrderName.of(orderDto.OrderName),
                shippingAddress: shippingAddress,
                billingAddress: BillingAddress,
                payment: Payment.of(orderDto.Payment.CardName, orderDto.Payment.CardNumber, orderDto.Payment.Expiration, orderDto.Payment.Cvv, orderDto.Payment.PaymentMethod)
                );
            foreach(var orderItemDto in orderDto.OrderItems)
            {
                newOrder.Add(ProductId.of(orderItemDto.ProductId),orderItemDto.Quantity,orderItemDto.Price);
            }

            return newOrder;    


        }
    }
}
