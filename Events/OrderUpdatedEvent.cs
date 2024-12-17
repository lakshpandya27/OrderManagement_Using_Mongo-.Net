using MongoDB.Bson;

public class OrderUpdatedEvent
{
    private string id;

    public string OrderId { get; set; }
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public ObjectId Id { get; }

    // Constructor with parameters
    public OrderUpdatedEvent(string orderId, string productName, int quantity, decimal price)
    {
        OrderId = orderId;
        ProductName = productName;
        Quantity = quantity;
        Price = price;
    }

    public OrderUpdatedEvent(ObjectId id, string productName, int quantity, decimal price)
    {
        Id = id;
        ProductName = productName;
        Quantity = quantity;
        Price = price;
    }

    public OrderUpdatedEvent(string id, string productName)
    {
        this.id = id;
        ProductName = productName;
    }
}
