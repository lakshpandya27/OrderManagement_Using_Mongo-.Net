namespace OrderManagementSystem.Handlers
{
    public class OrderUpdatedEventHandler
    {
        public void Handle(OrderUpdatedEvent eventMessage)
        {
            Console.WriteLine($"Order Updated...!");
        }
    }
}
