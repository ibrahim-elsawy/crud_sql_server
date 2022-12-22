using crud_sql_server.Models.Entity;

namespace crud_sql_server.Models.DTO
{
    public class OrderResponse
    {
        public int OrderId { get; set; }
        public float TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }
        public int AccountId { get; set; }
        public string UserId { get; set; }
	}
}