namespace OrderManagementAPI.Model
{
    public class Order
    {
        public string Id { get; set; }
        public string CustomerName { get; set; }
        public List<OrderItem> Items { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
