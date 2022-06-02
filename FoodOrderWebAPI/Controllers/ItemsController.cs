using System.Windows;
using Microsoft.AspNetCore.Mvc;
using Persistence;
using Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Persistence.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FoodOrderWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IFoodOrderService _service;

        public ItemsController(IFoodOrderService service)
        {
            _service = service;
        }

        // GET: api/<ItemsController>
        [HttpGet("{orderId}")]
        public ActionResult<IEnumerable<CartItemDTO>> GetItems(int orderId)
        {
            try
            {
                var cart = _service.GetCart(orderId);
                if (cart == null) return NotFound();
                var cartDto = cart.Select(i => i.ToDto()).ToList();
                foreach (var cartItem in cartDto)
                {
                    var item = _service.GetItem(cartItem.ItemId);
                    if (item == null) return NotFound();
                    var itemDto = item.ToDto();
                    cartItem.Item = itemDto;
                }

                return Ok(cartDto);
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine(e);
                return NotFound();
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult PostItem(ItemDTO itemDto)
        {
            var category = _service.GetCategoryById(itemDto.Category.Id);
            if (category == null) return NotFound();

            var item = new Item()
            {
                Id = itemDto.Id,
                Name = itemDto.Name,
                Price = itemDto.Price,
                Description = itemDto.Description,
                Spicy = itemDto.Spicy,
                Vegan = itemDto.Vegan,
                OrderedCnt = itemDto.OrderedCnt,
                Category = category
            };

            var items = _service.GetItems();
            foreach (var itemsItem in items)
            {
                if (itemsItem.Name == item.Name)
                    return StatusCode(StatusCodes.Status409Conflict);
            }

            var newItem = _service.CreateItem(item);

            if (newItem is null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok();
        }
    }
}
