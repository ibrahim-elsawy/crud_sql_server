using crud_sql_server.Models;
using crud_sql_server.Models.DTO;
using crud_sql_server.Models.Entity;
using crud_sql_server.Models.Repository;
using crud_sql_server.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace crud_sql_server.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class OrderController : ControllerBase
	{
		private readonly IOrderService _orderSerive;

		public OrderController(IOrderService orderSerive)
		{
			_orderSerive = orderSerive;
		}

		[Authorize(Roles = "Creator")]
		[HttpGet]
		[Route("budget")]
		public async Task<IActionResult> CheckMyBudget()
		{
			try
			{ 
				var username = HttpContext.User.Claims.First().Value;
				var state = await _orderSerive.CheckUserPayment(username);
				if (state)
				{
					return Ok(new Response()
					{
						StatusCode = "Success",
						Message = "didn't exceed the budget limit"
					});

				}
				return Ok(new Response() { 
					StatusCode = "Failed",
					Message = " Exceeded the budget limit"
				});
			}
			catch (System.Exception)
			{
				return BadRequest();
			}
		}

		[Authorize(Roles = "Creator, Viewer, Account")]
		[HttpGet]
		[Route("all")]
		public async Task<IActionResult> GetMyOrders()
		{
			try
			{
				var username = HttpContext.User.Claims.First().Value;
				var orders = await _orderSerive.GetUserAllOrders(username);
				return new JsonResult(new { orders = orders });
			}
			catch (System.Exception)
			{
				return BadRequest();
			}
		}

		[Authorize(Roles = "Admin")]
		[HttpGet]
		[Route("user")]
		public async Task<IActionResult> GetUserOrders([FromBody] UserOrderByUsername userOrder)
		{
			try
			{
				var orders = await _orderSerive.GetUserAllOrders(userOrder.username);
				return new JsonResult(new { orders = orders });
			}
			catch (System.Exception)
			{
				return BadRequest();
			}

		}
		// TODO: solve that any Role can query any order details
		[Authorize(Roles = "Admin, Creator, Viewer, Account")]
		[HttpGet]
		[Route("details/{orderid}")]
		public async Task<IActionResult> GetOrdersDetails([FromRoute] int OrderId)
		{
			try
			{
				var orders = await _orderSerive.GetOrderDetails(OrderId);
				return new JsonResult(new { orders_details = orders });
			}
			catch (System.Exception)
			{
				return BadRequest();
			}

		}


		[Authorize(Roles = "Creator")]
		[HttpPost]
		public async Task<IActionResult> CreateOrder(OrderRegistration orderRegistration)
		{
			var username = HttpContext.User.Claims.First().Value;
			var state = await _orderSerive.CreateOrder(username, orderRegistration);
			if (state.State)
			{
				return Created("/order", orderRegistration);
			}

			return BadRequest(state);
		}

	}
}