namespace MyBlazorApp.Model;

public class Order
{
    public int OrderId { get; set; }
    public string OrderName { get; set; }
    public List<OrderItem> Items { get; set; } = new List<OrderItem>();
}

public class OrderItem
{
    public int OrderItemId { get; set; }
    public string ItemName { get; set; }
    public int Quantity { get; set; }
}

