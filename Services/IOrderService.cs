using crud_sql_server.Models.DTO;
using crud_sql_server.Models.Entity;

namespace crud_sql_server.Services
{
    public interface IOrderService
    {
		Task<float> GetUserPayment(string username);
		Task<bool> CheckUserPayment(string username);
		Task<OrderServiceCreation> CreateOrder(string username, OrderRegistration orderRegistration);
		Task<IEnumerable<OrderResponse>> GetUserAllOrders(string username);
		Task<IEnumerable<OrderDetailsResponse>> GetOrderDetails(int OrderId);
	}
}