using CalculateCartMicroservice.Entity;
using CalculateCartMicroservice.Entity.Model;
using CalculateCartMicroservice.Logger;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CalculateCartMicroservice.Repository
{
    public class CartRepo : ICartRepo
    {
        private readonly CartDBContext _cartDBContext;
        private readonly IConfiguration _configuration;
        private readonly ILoggerManager _logger;

        public CartRepo(IConfiguration configuration,CartDBContext cartDBContext,ILoggerManager logger)
        {
            _configuration= configuration;
            _cartDBContext = cartDBContext;
            _logger = logger;
        }

        public string AddOrderDetails(UserOrderDetails userOrder)
        {
            try
            {
                _cartDBContext.UserOrderDetails.Add(userOrder);
                _cartDBContext.SaveChanges();
                return "successful";
            }
            catch (Exception ex) {
                //logger
                _logger.LogInformation("Error at "+nameof(AddOrderDetails)+" Exception :"+ ex.Message);
            return ex.Message;
            }
            

        }

        public async Task<TotalValue> CalculateTotalValueAsync(UserOrderDetails userOrderDetails)
        {
            
            FoodItem foodItem= new FoodItem();
            TotalValue totalValue = new TotalValue();
            _logger.LogInformation(nameof(CalculateCartMicroservice)+" Calculating the Total Value of order id = " + userOrderDetails.OrderId);
            try
            {
                using (var httpClient = new HttpClient())
                {                   
                    var fetchFood = _configuration["GetFoodItemDetails"];
                    if (userOrderDetails.FoodItemList != null && userOrderDetails.FoodItemList.Any() == true)
                    {
                        foreach (FoodItemDetails foodDetails in userOrderDetails.FoodItemList)
                        {
                            if (foodDetails.FoodName != null)
                            {
                                using (var response = await httpClient.GetAsync(fetchFood + foodDetails.FoodName))
                                {
                                    _logger.LogInformation("Fetching the details of food " + foodDetails.FoodName + "from the food price api");
                                    string apiResponse = await response.Content.ReadAsStringAsync();
                                    
                                    Console.WriteLine(apiResponse);
                                    foodItem = JsonConvert.DeserializeObject<FoodItem>(apiResponse);
                                    _logger.LogInformation("The Food Item details are " + JsonConvert.SerializeObject(foodItem));
                                    Console.WriteLine(foodItem);
                                }
                                Console.WriteLine(foodItem);
                                totalValue.Value += foodDetails.Quantity * foodItem.FoodPrice;

                            }
                        }
                    }
                   
                }
                totalValue.Value = Math.Round(totalValue.Value, 2);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("At "+nameof(CalculateTotalValueAsync)+"Exception occured while calculating the total value of order id" + userOrderDetails.OrderId + ":" + ex.Message);
            }
            return totalValue;
        }
    

        

        public List<UserOrderDetails> GetAllOrdersByUserId(int id)
        {
            List<UserOrderDetails> userOrderDetails = _cartDBContext.UserOrderDetails.Include(a => a.FoodItemList).Where(a=>a.UserId==id).OrderByDescending(a=>a.OrderId).ToList();
            return userOrderDetails;
        }

        public UserOrderDetails GetUserOrderDetailsByOrderId(long id)
        {
            UserOrderDetails userOrderDetails = new UserOrderDetails();
            try
            {
                userOrderDetails = _cartDBContext.UserOrderDetails.Include(a=>a.FoodItemList).FirstOrDefault(e => e.OrderId == id);
            }
            catch(Exception ex)
            {
                _logger.LogInformation("Error at " + nameof(GetUserOrderDetailsByOrderId) +" Exception :"+ex.Message);



            }
            return userOrderDetails;
        }
        

    }
}
