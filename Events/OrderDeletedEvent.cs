public class OrderDeletedEvent
{
    public string OrderId { get; set; }

    // Constructor with parameters
    public OrderDeletedEvent(string orderId)
    {
        OrderId = orderId;
    }
}
