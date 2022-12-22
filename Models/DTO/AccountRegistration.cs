using System.ComponentModel.DataAnnotations;

namespace crud_sql_server.Models.DTO
{
	public class AccountRegistration
	{
		[Required]
		public string Name { get; set; }

	}
}