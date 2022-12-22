using System.ComponentModel.DataAnnotations;

namespace crud_sql_server.Models.DTO
{
    public class ProductDTO
    {
        [Required]
        public string ProductName { get; set; }
        [Required]
        public int Quantity { get; set; }
	}
}