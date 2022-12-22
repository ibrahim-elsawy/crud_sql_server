using System.ComponentModel.DataAnnotations;

namespace crud_sql_server.Models.DTO
{
	public class UserLoginRequest
	{
		[Required]
		[EmailAddress]
		public string Email { get; set; }
		[Required]
		public string Password { get; set; }
	}
}