namespace Ordering.Infrastructure.Extensions
{
    internal class InitialData
    {
        public static IEnumerable<Customer> Customers =>
            new List<Customer>
            {
                Customer.Create(CustomerId.of(new Guid("f01d79c5-4ea6-4aa4-a3d2-047781975436")),"John","John@gmail.com"),
                Customer.Create(CustomerId.of(new Guid("17ed2ae3-e120-468d-902d-7fd68aca87a4")),"kaz","kaz@gmail.com"),
            };

        public static IEnumerable<Product> Products =>
            new List<Product>
            {
                Product.Create(ProductId.of(new Guid("85d89e90-c1d8-4d15-8548-0deb5b936a24")),"Iphone",500),
                Product.Create(ProductId.of(new Guid("4c30a3fd-3da3-4ce0-b6ed-1b8e8ed4a891")),"SamsungA55",400),
                Product.Create(ProductId.of(new Guid("834de9c6-069d-4fe3-88b3-d98a6d582695")),"Huwawe",650),
                Product.Create(ProductId.of(new Guid("a105e58e-2e85-4178-9588-57b2b704e506")),"Redmi",350),

            };

        public static IEnumerable<Order> OrdersWithItems 
                    
        {

            get{
                var address1 = Address.of("John", "Mu", "john@gmail.com","0112568", "USA", "Canada", "12456");
                var address2 = Address.of("kaz", "doe", "kazdoes@gmail.com","0123547", "USA", "broadway", "45623");

                var payment1 = Payment.of("kaz", "00111", "12/28", "544", 1);
                var payment2 = Payment.of("John", "00222", "8/28", "189", 2);

                var Order1 = Order.Create(
                    OrderId.of(Guid.NewGuid()),
                     CustomerId.of(new Guid("f01d79c5-4ea6-4aa4-a3d2-047781975436")),
                     OrderName.of("ORD_1"),
                     shippingAddress: address1,
                     billingAddress: address1,
                     payment2);
                Order1.Add(ProductId.of(new Guid("85d89e90-c1d8-4d15-8548-0deb5b936a24")), 2, 500);
                Order1.Add(ProductId.of(new Guid("4c30a3fd-3da3-4ce0-b6ed-1b8e8ed4a891")), 1, 400);


                var Order2 = Order.Create(
                 OrderId.of(Guid.NewGuid()),
                  CustomerId.of(new Guid("17ed2ae3-e120-468d-902d-7fd68aca87a4")),
                  OrderName.of("ORD_2"),
                  shippingAddress: address2,
                  billingAddress: address2,
                  payment1);
                Order1.Add(ProductId.of(new Guid("a105e58e-2e85-4178-9588-57b2b704e506")), 1, 350);
                Order1.Add(ProductId.of(new Guid("834de9c6-069d-4fe3-88b3-d98a6d582695")), 1, 650);


                return new List<Order> { Order1, Order2};


            


            }
         }

    }
}
