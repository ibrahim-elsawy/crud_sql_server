using crud_sql_server.Config;
using crud_sql_server.Models.DTO;
using crud_sql_server.Models.Entity;
using crud_sql_server.Models.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace crud_sql_server.Controllers;

[Authorize(Roles = "Admin")]
[ApiController]
[Route("api/[controller]")]
public class AdminController : ControllerBase
{
	private readonly ILogger<AdminController> _logger;
	private readonly UserManager<ApplicationUser> userManager;
	private readonly RoleManager<IdentityRole> roleManager;
	private readonly IAccountRepo _accountRepo;

	public AdminController(ILogger<AdminController> logger, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IAccountRepo accountRepo)
	{
		this.userManager = userManager;
		this.roleManager = roleManager;
		_logger = logger;
		_accountRepo = accountRepo;
	}


	[HttpGet]
	[Route("accounts")]
	public async Task<IActionResult> GetAllAccounts()
	{
		//list the the users that have account Role (e.g admin in the account or the company)
		// var accounts = await this.userManager.GetUsersInRoleAsync(UserRoles.Account);

		//list all accounts 
		var accounts = await _accountRepo.GetAccounts();
		return new JsonResult(new { accounts = accounts});
	}


	[HttpGet]
	[Route("users")]
	public async Task<IActionResult> GetAllUsers()
	{
		var viewers = await this.userManager.GetUsersInRoleAsync(UserRoles.ViewerUser);
		var v = viewers.Select(u => new { username = u.UserName, phone = u.PhoneNumber, email = u.Email });
		var creators = await this.userManager.GetUsersInRoleAsync(UserRoles.CreatorUser);
		var c = creators.Select(u => new { username = u.UserName, phone = u.PhoneNumber, email = u.Email });

		return new JsonResult(new { users = c.Concat(v) }) ;
	}

	[HttpPost]
	[Route("account")]
	public async Task<IActionResult> AddAccount([FromBody] AccountRegistration model)
	{
		try
		{ 
			await _accountRepo.CreateAccount(model); 
			return Created("api/admin/account",model);
		}
		catch (System.Exception)
		{
			
			return BadRequest();
		} 
	}

	[HttpPost]
	[Route("to-account")]
	public async Task<IActionResult> AddUserToAccount([FromBody] UserToAccount userToAccount)
	{
		try
		{
			await _accountRepo.AddUserToAccount(userToAccount);
			return Created("/to-account", userToAccount);
		}
		catch (System.Exception)
		{
			return BadRequest();
		}
	}
}
