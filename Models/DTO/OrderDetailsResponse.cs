namespace crud_sql_server.Models.DTO
{
    public class OrderDetailsResponse
    {
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public float Price { get; set; }
        public float TotalPrice { get; set; }
	}
}