using BuildingBlocks.Messaging.Events;
using MassTransit;
using Ordering.Application.Dtos;
using Ordering.Application.Orders.Commands.CreateOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Orders.EventHandlers.Integration
{
    internal class BascketCheckoutEventHandler(ISender sender,ILogger<BascketCheckoutEventHandler>logger) : IConsumer<BasketCheckoutEvent>
    {
        public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
        {
            logger.LogInformation("Integration Event Handled:{IntegrationEvent}",context.Message.GetType().Name);
            var command = MapToCreateCommand(context.Message);

           await sender.Send(command);
        }

        private CreateOrderCommand MapToCreateCommand(BasketCheckoutEvent message)
        {
            var paymentDto = new PaymentDto(message.CardName,message.CardNumber,message.Expiration,message.CVV,message.PaymentMethod);
            var addressDto = new AddressDto(message.FirstName,message.LastName,message.EmailAddress,message.AddressLine,message.Country,message.State,message.ZipCode);
            var orderId = Guid.NewGuid();

            var orderDto = new OrderDto(
                Id: orderId,
                CustomerId: message.CustomerId,
                OrderName: message.UserName,
                ShippingAddress: addressDto,
                BillingAddress: addressDto,
                Payment: paymentDto,
                Status: Ordering.Domain.Enums.OrderStatus.Pending,
                OrderItems: [
                    new OrderItemDto(orderId,new Guid("4c30a3fd-3da3-4ce0-b6ed-1b8e8ed4a891"),1,400),
                    new OrderItemDto(orderId,new Guid("85d89e90-c1d8-4d15-8548-0deb5b936a24"),2,500),

                    ]
                );
            return new CreateOrderCommand(orderDto);

             
        }
    }
   
}
