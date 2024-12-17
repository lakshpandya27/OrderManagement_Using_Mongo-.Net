


namespace OrderManagement.Handlers
{
    public class OrderDeletedEventHandler
    {
        public void Handle(OrderCreatedEvent eventMessage)
        {
            Console.WriteLine($"Order Deleted...!");
        }

        internal void Handle(OrderDeletedEvent orderDeletedEvent)
        {
            throw new NotImplementedException();
        }
    }
}
