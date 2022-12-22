using crud_sql_server.Models.DTO;
using crud_sql_server.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace crud_sql_server.Models.DAO
{
	public class AccountDAO : IAccountDAO
	{
		private readonly ApplicationDbContext _context;

		public AccountDAO(ApplicationDbContext context)
		{
			_context = context;
		}


		public async Task<int> CreateAccount(AccountRegistration accountRegistration)
		{
			var acc = new Account() { Name = accountRegistration.Name};
			await _context.AddAsync<Account>(acc);
			var state = await _context.SaveChangesAsync();
			return state;
		}

		public async Task<Account> GetAccountByName(string name)
		{
			var acc = await _context.Accounts.Where(e => e.Name == name).FirstOrDefaultAsync();
			return acc;
		}

		public async Task<IEnumerable<Account>> GetAccounts()
		{
			var accounts = await _context.Accounts.ToListAsync();
			return accounts;
		}
	}
}