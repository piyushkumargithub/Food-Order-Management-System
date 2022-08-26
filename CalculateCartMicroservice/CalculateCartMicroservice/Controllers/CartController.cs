using CalculateCartMicroservice.Entity.Model;
using CalculateCartMicroservice.Logger;
using CalculateCartMicroservice.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CalculateCartMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [Authorize]
    public class CartController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly ICartRepo _cartRepo;
        public CartController(ICartRepo cartRepo,ILoggerManager logger)
        {
            _cartRepo = cartRepo;
            _logger = logger;

        }


        // GET api/<CartController>/5
        [HttpGet("getbyorder/{id}")] //Will be for admin role
        public UserOrderDetails GetByOrderId(long id)
        {
            if (id == 0)
            {
                _logger.LogInformation(nameof(GetByOrderId)+"method invoked with 0 as OrderID");
                return null;
            }
            var value = _cartRepo.GetUserOrderDetailsByOrderId(id);
            return value;
        }

        [HttpGet("{id}")]
        public IActionResult GetByUserId(int id)
        {
            if (id == 0)
            {
                _logger.LogInformation(nameof(GetByUserId) + "method invoked with 0 as UserID");
            }
            var value = _cartRepo.GetAllOrdersByUserId(id);
            return Ok(value);
        }


        // POST api/<CartController>
        [HttpPost]
        public ActionResult<string> CreateOrder(UserOrderDetails userOrder)
        {
            if (userOrder.FoodItemList.Count == 0)
            {
                _logger.LogInformation(nameof(CreateOrder)+" no food in list");
                return Ok("Add atleast 1 Food item");
            }
            var value=_cartRepo.AddOrderDetails(userOrder);
            return Ok(value);
        }

        [HttpPost("TotalValue")]
        public IActionResult TotalValue(UserOrderDetails userOrderDetails)
        {
            TotalValue totalValue = new TotalValue();
            //log
            try
            {
                if (userOrderDetails == null)
                {
                    _logger.LogInformation(nameof(TotalValue) + " The Order doesn't contain any data");
                    return NotFound("The Order doesn't contain any data");
                }  
                else
                {
                    _logger.LogInformation(nameof(TotalValue) + " calculating total value of order ");
                    totalValue = _cartRepo.CalculateTotalValueAsync(userOrderDetails).Result;
                    _logger.LogInformation(nameof(TotalValue)+" calculated value for order is"+totalValue.Value);

                    return Ok(totalValue);
                }
            }
            catch(Exception ex)
            {
                _logger.LogInformation("Error occured from "+nameof(TotalValue)+"Error Message:"+ex.Message); 
                return new StatusCodeResult(500);
            }
        }
    }
}
