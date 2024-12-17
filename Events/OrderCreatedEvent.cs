using MongoDB.Bson;

public class OrderCreatedEvent
{
    public string OrderId { get; }
    public string ProductName { get; }
    public int Quantity { get; }
    public decimal Price { get; }
    public ObjectId Id { get; }

    // Constructor with parameters
    public OrderCreatedEvent(string orderId, string productName, int quantity, decimal price)
    {
        OrderId = orderId;
        ProductName = productName;
        Quantity = quantity;
        Price = price;
    }

    public OrderCreatedEvent(ObjectId id, string productName, int quantity, decimal price)
    {
        Id = id;
        ProductName = productName;
        Quantity = quantity;
        Price = price;
    }

    public OrderCreatedEvent(ObjectId id)
    {
        Id = id;
    }
}
