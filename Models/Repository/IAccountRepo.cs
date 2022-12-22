using crud_sql_server.Models.DTO;
using crud_sql_server.Models.Entity;

namespace crud_sql_server.Models.Repository
{
	public interface IAccountRepo
	{
		Task<IEnumerable<Account>> GetAccounts();
		Task<int> CreateAccount(AccountRegistration accountRegistration);
		Task<int> AddUserToAccount(UserToAccount userToAccount);
	}
}