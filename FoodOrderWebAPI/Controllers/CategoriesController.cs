using Microsoft.AspNetCore.Mvc;
using Persistence;
using Data;
using Persistence.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FoodOrderWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IFoodOrderService _service;

        public CategoriesController(IFoodOrderService service)
        {
            _service = service;
        }

        // GET: api/<OrdersController>
        [HttpGet]
        public IActionResult GetCategories()
        {
            try
            {
                return Ok(_service
                    .GetCategories().ToList()
                    .Select(cat => cat.ToDto()));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // GET api/<OrdersController>/5
        [HttpGet("{id}")]
        public IActionResult GetCategory(int id)
        {
            try
            {
                var cat = _service.GetCategoryById(id);
                if (cat == null)
                    return NotFound();
                return Ok(cat.ToDto());
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
