using crud_sql_server.Models.DTO;
using crud_sql_server.Models.Entity;

namespace crud_sql_server.Models.DAO
{
    public interface IAccountDAO
    {
		Task<IEnumerable<Account>> GetAccounts();
		Task<int> CreateAccount(AccountRegistration accountRegistration);
		Task<Account> GetAccountByName(string name);
	}
}