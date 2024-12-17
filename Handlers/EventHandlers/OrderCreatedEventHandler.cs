namespace OrderManagement.Handlers
{
    public class OrderCreatedEventHandler
    {
        public void Handle(OrderCreatedEvent eventMessage)
        {
            Console.WriteLine($"Order Created...!");
        }
    }
}
