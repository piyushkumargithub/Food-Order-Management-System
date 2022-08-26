using FoodPrice.Logger;
using FoodPrice.Repository;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FoodPrice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IFoodRepo _foodRepo;
        public FoodController(IFoodRepo foodRepo,ILoggerManager logger)
        {
            _foodRepo = foodRepo;
            _logger = logger;
        }
        

        // GET api/<FoodController>/5
        [HttpGet("{foodName}")]
        public IActionResult GetFoodPrice(string foodName)
        {
            if (string.IsNullOrEmpty(foodName))
            {
                _logger.LogInformation(nameof(GetFoodPrice) + "method invoked without food name");
                return BadRequest();
            }
            var food = _foodRepo.GetFoodByName(foodName);
            if (food == null)
            {
                _logger.LogInformation(nameof(GetFoodPrice) + " food "+foodName+" doesn't exist in database");
                return NoContent();
            }
            return Ok(food);
        }

        
    }
}
