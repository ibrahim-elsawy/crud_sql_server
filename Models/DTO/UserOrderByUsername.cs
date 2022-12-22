using System.ComponentModel.DataAnnotations;

namespace crud_sql_server.Models.DTO
{
    public class UserOrderByUsername
    {
        [Required]
        public string username { get; set; }
	}
}