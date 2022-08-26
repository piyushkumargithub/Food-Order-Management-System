using FoodPrice.Controllers;
using FoodPrice.Entity.Model;
using FoodPrice.Logger;
using FoodPrice.Repository;
using Moq;
using NUnit.Framework;

namespace FoodPriceTesting
{
    public class Tests
    {
        private readonly Mock<ILoggerManager> mockLogger = new Mock<ILoggerManager>();
        private readonly Mock<IFoodRepo> mockRepo = new Mock<IFoodRepo>();
        public FoodController foodController;
        public string foodName;
        public Tests()
        {
            foodController = new FoodController(mockRepo.Object, mockLogger.Object);
        }

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void GetFoodPrice_noFoodName()
        {
            foodName = "";
            var result=foodController.GetFoodPrice(foodName);
            Assert.That(result.ToString(), Is.EqualTo("Microsoft.AspNetCore.Mvc.BadRequestResult"));
        }
        [Test]
        public void GetFoodPrice_correctFoodName()
        {
            foodName = "Samosa";
            mockRepo.Setup(x => x.GetFoodByName(foodName)).Returns(new FoodItem() { FoodId = 1, FoodName = "Samosa", FoodPrice = 20 });
            var result= foodController.GetFoodPrice(foodName);
            var price = result.ToString();
            Assert.That(price, Is.EqualTo("Microsoft.AspNetCore.Mvc.OkObjectResult"));
        }
        [Test]
        public void GetFoodPrice_wrongFoodName()
        {
            foodName = "Samosaa";
            mockRepo.Setup(x => x.GetFoodByName(foodName)).Returns(()=>null);
            var result = foodController.GetFoodPrice(foodName);
            var price = result.ToString();
            Assert.That(price, Is.EqualTo("Microsoft.AspNetCore.Mvc.NoContentResult"));
        }

    }
}