using crud_sql_server.Models;
using crud_sql_server.Models.DTO;
using crud_sql_server.Models.Entity;
using crud_sql_server.Models.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace crud_sql_server.Services
{
	public class OrderService : IOrderService
	{
		private const float Budget = 1000;
        private const float Price = 100;

		private readonly UserManager<ApplicationUser> _userManager;
		private readonly ApplicationDbContext _context;

		public OrderService(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
		{
			_userManager = userManager;
			_context = context;
		}

		public async Task<bool> CheckUserPayment(string username)
		{ 
            var user = await _userManager.FindByNameAsync(username); 
            var totalPrice = await _getUserPayment(user);
			return (totalPrice < Budget) ? true : false;
		}

		public async Task<OrderServiceCreation> CreateOrder(string username, OrderRegistration orderRegistration)
		{
			try
			{
				var user = await _userManager.FindByNameAsync(username);
				await _context.Entry(user).Reference(u => u.Account).LoadAsync();
				var acc = user.Account;
				if (acc == null)
				{
					return new OrderServiceCreation(){
                        State = false,
                        Message = "The user doen't belong to any Account"
                    };
				}
				var creation = await _CreatOrder(user, acc, orderRegistration);
                if (creation)
                {
					return new OrderServiceCreation() { 
                        State = true,
                        Message = "Order created successfuly"
                    };
				}
				return new OrderServiceCreation() { 
                    State = false,
                    Message = "You have exceeded the Budget for this month..."
                };


			}
			catch (System.Exception)
			{
				return new OrderServiceCreation(){
                    State = false,
                    Message = "Bad request"
                };
			}
		}



		private async Task<bool> _CreatOrder(ApplicationUser user, Account account, OrderRegistration orderRegistration)
		{
            try
            { 
                var order = new Order() 
                { 
                    TotalPrice = 0,
                    OrderDate = DateTime.Now
                };
                foreach (var product in orderRegistration.Orders)
                {
                    var orderDetails = new OrderDetails() { 
                        ProductName = product.ProductName,
                        Price = Price,
                        Quantity = product.Quantity,
                        TotalPrice = product.Quantity * Price,
                    };
                    order.TotalPrice += product.Quantity * Price;
                    order.OrderDetails.Add(orderDetails);
                }
				var currentPayment = await _getUserPayment(user);
                if ((currentPayment + order.TotalPrice) > Budget)
                {
					return false;
				}
				user.Orders.Add(order);
                account.Orders.Add(order);
				await _context.SaveChangesAsync();
				return true;

			}
            catch (System.Exception)
            {
				return false;
			}
			
            
		}
		
		public async Task<float> GetUserPayment(string username)
		{ 
            var user = await _userManager.FindByNameAsync(username);
			return await _getUserPayment(user);
		}
        
		private async Task<float> _getUserPayment(ApplicationUser user)
        {
			await _context.Entry(user).Collection(u => u.Orders).LoadAsync();
			var date = DateTime.Now;
			var currentMonth = date.Month;
			var currentYear = date.Year;
			var totalPrice = user.Orders
                                    .Where(o => o.OrderDate.Month == currentMonth && o.OrderDate.Year == currentYear)
                                    .Select(o => o.TotalPrice)
                                    .Sum();

			return totalPrice;

		}
        public async Task<IEnumerable<OrderDetailsResponse>> GetOrderDetails(int OrderId)
        {
			var orderDetails = await _context.OrderDetails.Where(o => o.OrderId == OrderId).Select(o => new OrderDetailsResponse{
                ProductName = o.ProductName,
                Quantity = o.Quantity,
                Price = o.Price,
                TotalPrice = o.TotalPrice
            }).ToListAsync();

			return orderDetails;
		}
		public async Task<IEnumerable<OrderResponse>> GetUserAllOrders(string username)
		{ 

            var user = await _userManager.FindByNameAsync(username);
            await _context.Entry(user).Collection(u => u.Orders).LoadAsync();
            // await _context.Entry(user).Collection(u => u.Orders.Select(o => o.OrderDetails)).LoadAsync();
			var orders = user.Orders.Select( o => new OrderResponse {
					OrderId = o.OrderId,
					TotalPrice = o.TotalPrice,
					UserId = o.UserId,
					AccountId = o.AccountId,
					OrderDate = o.OrderDate,
				}
			);
			return orders;
		}
	}
}