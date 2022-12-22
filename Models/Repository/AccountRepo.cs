using crud_sql_server.Models.DAO;
using crud_sql_server.Models.DTO;
using crud_sql_server.Models.Entity;
using Microsoft.AspNetCore.Identity;

namespace crud_sql_server.Models.Repository
{
	public class AccountRepo : IAccountRepo
	{
		private readonly IAccountDAO _accoutDAO;
		private readonly UserManager<ApplicationUser> userManager;
		private readonly ApplicationDbContext _context;



		public AccountRepo(IAccountDAO accoutDAO, UserManager<ApplicationUser> userManager, ApplicationDbContext context)
		{
			_accoutDAO = accoutDAO;
			this.userManager = userManager;
			_context = context;
		}

		public async Task<int> AddUserToAccount(UserToAccount userToAccount)
		{
			var user = await this.userManager.FindByEmailAsync(userToAccount.Email);
			var acc = await this._accoutDAO.GetAccountByName(userToAccount.account_name);
			acc.Users.Add(user);
			var state = await _context.SaveChangesAsync();
			return state;
		}

		public async Task<int> CreateAccount(AccountRegistration accountRegistration)
		{
			return await _accoutDAO.CreateAccount(accountRegistration);
		}

		public async Task<IEnumerable<Account>> GetAccounts()
		{
			return await _accoutDAO.GetAccounts();
		}
	}
}