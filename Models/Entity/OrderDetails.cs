namespace crud_sql_server.Models.Entity
{
    public class OrderDetails
    {
        public int OD_Id { get; set; }
        public string ProductName { get; set; }
		public float Price { get; set;}
        public int Quantity { get; set; }
		public float TotalPrice { get; set; }
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }


	}
}