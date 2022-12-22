using System.ComponentModel.DataAnnotations;

namespace crud_sql_server.Models.DTO
{
    public class OrderRegistration
    {
        [Required]
		public IEnumerable<ProductDTO> Orders { get; set; }
	}
}