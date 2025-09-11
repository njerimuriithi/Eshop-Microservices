namespace Shopping.Web.Services
{
    public interface IOrderingService
    {
        [Get("/ordering-service/orders?pageIndex={pageIndex}&pageSize={pageSize}\"")]
        Task<GetOrdersResponse>GetOrder(int?pageIndex=1,int?pageSize=10);
            [Get("/ordering-service/orders/{orderName}")]
        Task<GetOrdersByNameResponse>GetOrderByName(string orderName);
            [Get("/ordering-service/orders/customer/{customerId}")]
        Task<GetOrdersByCustomerResponse>GetOrderByCustomer(Guid customerId);
    }
}
