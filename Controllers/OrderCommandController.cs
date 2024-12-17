using Microsoft.AspNetCore.Mvc;
using OrderManagement.Handlers.CommandHandlers;
using OrderManagement.Handlers;
using OrderManagement.Models;
using OrderManagementSystem.Handlers;

namespace OrderManagement.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrderCommandController : ControllerBase
    {
        private readonly OrderCommandService _orderCommandService;
        private readonly OrderCreatedEventHandler _orderCreatedEventHandler;
        private readonly OrderUpdatedEventHandler _orderUpdatedEventHandler;
        private readonly OrderDeletedEventHandler _orderDeletedEventHandler;

        public OrderCommandController(
            OrderCommandService orderCommandService,
            OrderCreatedEventHandler orderCreatedEventHandler,
            OrderUpdatedEventHandler orderUpdatedEventHandler,
            OrderDeletedEventHandler orderDeletedEventHandler)
        {
            _orderCommandService = orderCommandService;
            _orderCreatedEventHandler = orderCreatedEventHandler;
            _orderUpdatedEventHandler = orderUpdatedEventHandler;
            _orderDeletedEventHandler = orderDeletedEventHandler;
        }

        // POST: api/orders
        [HttpPost]
        public IActionResult CreateOrder([FromBody] Order order)
        {
            // Create the order using the command service
            _orderCommandService.CreateOrder(order);

            // Trigger the OrderCreated event handler
            _orderCreatedEventHandler.Handle(new OrderCreatedEvent(order.Id));

            // Return the created order's URL using GetOrder from the QueryController
            return CreatedAtAction(
                actionName: nameof(OrderQueryController.GetOrder), // Reference to the GetOrder method in OrderQueryController
                controllerName: "OrderQuery", // Optional: specify the controller if necessary
                routeValues: new { id = order.Id }, // Pass the id to build the URL
                value: order // Return the created order
            );
        }

        // PUT: api/orders/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateOrder(string id, [FromBody] Order order)
        {
            _orderCommandService.UpdateOrder(id, order);

            // Trigger the OrderUpdated event handler
            _orderUpdatedEventHandler.Handle(new OrderUpdatedEvent(id, order.ProductName));

            return NoContent();
        }

        // DELETE: api/orders/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteOrder(string id)
        {
            _orderCommandService.DeleteOrder(id);

            // Trigger the OrderDeleted event handler
            _orderDeletedEventHandler.Handle(new OrderDeletedEvent(id));

            return NoContent();
        }
    }
}
