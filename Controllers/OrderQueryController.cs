using Microsoft.AspNetCore.Mvc;
using OrderManagement.Handlers.QueryHandlers;
using OrderManagement.Models;

namespace OrderManagement.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrderQueryController : ControllerBase
    {
        private readonly OrderQueryService _orderQueryService;

        public OrderQueryController(OrderQueryService orderQueryService)
        {
            _orderQueryService = orderQueryService;
        }

        // GET: api/orders
        [HttpGet]
        public IActionResult GetAllOrders()
        {
            var orders = _orderQueryService.GetAllOrders();
            return Ok(orders);
        }

        // GET: api/orders/{id}
        [HttpGet("{id}")]
        public IActionResult GetOrder(string id)
        {
            var order = _orderQueryService.GetOrderById(id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }
    }
}
