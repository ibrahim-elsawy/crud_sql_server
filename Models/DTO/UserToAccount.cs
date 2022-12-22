using System.ComponentModel.DataAnnotations;

namespace crud_sql_server.Models.DTO
{
    public class UserToAccount
    {
        [Required]
        public string Email {get; set;}
        [Required]
        public string account_name { get; set; }
	}
}