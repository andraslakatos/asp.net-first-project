using Microsoft.AspNetCore.Mvc;
using Persistence;
using Data;
using Persistence.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FoodOrderWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IFoodOrderService _service;

        public OrdersController(IFoodOrderService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetOrders()
        {
            try
            {
                return Ok(_service
                    .GetOrders()
                    .Select(order => order.ToDto()));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetOrders(int id)
        {
            try
            {
                var order = _service.GetOrder(id);
                return Ok(order.ToDto());
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine(e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut("{id}")]
        public ActionResult PutOrders(int id, OrderDTO orderDto)
        {
            if (id != orderDto.Id)
            {
                return BadRequest();
            }

            var newOrder = new Order()
            {
                Id = orderDto.Id,
                CartItems = orderDto.CartItems,
                Name = orderDto.Name,
                Address = orderDto.Address,
                PhoneNumber = orderDto.PhoneNumber,
                TimeOfOrder = orderDto.TimeOfOrder,
                TimeOfCompletion = orderDto.TimeOfCompletion,
                TotalPrice = orderDto.TotalPrice
            };

            if (_service.UpdateOrder(newOrder))
            {
                return Ok();
            }

            return StatusCode(StatusCodes.Status500InternalServerError);
        }

    }
}
