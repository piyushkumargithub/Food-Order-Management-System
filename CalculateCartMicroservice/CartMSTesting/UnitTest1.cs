using CalculateCartMicroservice.Controllers;
using CalculateCartMicroservice.Entity;
using CalculateCartMicroservice.Entity.Model;
using CalculateCartMicroservice.Logger;
using CalculateCartMicroservice.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CartMSTesting
{
    public class Tests
    {
        private readonly Mock<ILoggerManager> mockLogger = new Mock<ILoggerManager>();
        public List<UserOrderDetails> _userOrderDetails;
        public CartRepo _cartRepo;
        public readonly Mock<IConfiguration> mockConf = new Mock<IConfiguration>();
        public readonly Mock<CartDBContext> mockContext= new Mock<CartDBContext>();


        private readonly Mock<ICartRepo> mockCartRepo= new Mock<ICartRepo>();      
        public CartController _cartController;

        public Tests()
        {
            _cartController = new CartController(mockCartRepo.Object,mockLogger.Object);
            //_cartRepo = new CartRepo(mockConf.Object, mockContext.Object, mockLogger.Object);
        }

        [SetUp]
        public void Setup()
        {
            _userOrderDetails = new List<UserOrderDetails>
            {
                new UserOrderDetails { OrderId = 1,
                    UserId = 1,
                    FoodItemList = new List<FoodItemDetails>{
                        new FoodItemDetails {
                            FoodId = 1,
                            FoodName = "Pizza",
                            Quantity = 2
                        },
                        new FoodItemDetails {
                            FoodId = 3,
                            FoodName = "Pani Puri",
                            Quantity = 20
                        }
                    }

                },
                new UserOrderDetails { OrderId = 2,
                    UserId = 2,
                    FoodItemList = new List<FoodItemDetails>{
                        new FoodItemDetails {
                            FoodId = 5,
                            FoodName = "Sandwich",
                            Quantity = 2
                        }
                    }

                },

            };

        }
        //[Test]
        //public void AddOrderDetails_returns_successful()
        //{
        //    mockContext.Setup(x => x.UserOrderDetails.Add(It.IsAny<UserOrderDetails>()));
        //    mockContext.Setup(x => x.SaveChanges());
        //    var result = _cartRepo.AddOrderDetails(_userOrderDetails[0]);
        //    Assert.That(result, Is.EqualTo("successful"));
        //}
        //[Test]
        //public void AddOrderDetails_returns_exception()
        //{
        //    mockContext.Setup(x => x.UserOrderDetails.Add(It.IsAny<UserOrderDetails>()));
        //    mockContext.Setup(x => x.SaveChanges()).Returns(() => 1);
        //    var result = _cartRepo.AddOrderDetails(_userOrderDetails[0]);
        //    Assert.That("successful", Is.Not.EqualTo(result));
        //}

        [Test]
        public void GetByOrderId_ZeroId()
        {
            var result = _cartController.GetByOrderId(0);
           // Assert.That(ReferenceEquals(null,result));
            Assert.That(result, Is.Null);
        }
        [Test]
        public void GetByOrderId_NonZeroId()
        {
            mockCartRepo.Setup(x=> x.GetUserOrderDetailsByOrderId(1)).Returns(_userOrderDetails[0]);
            var result= _cartController.GetByOrderId(1);
           // Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.EqualTo(_userOrderDetails[0]));
        }

        [Test]
        public void GetByUserId_ZeroId()
        {
            var result=_cartController.GetByUserId(0) as ObjectResult;
            Assert.That(result.Value, Is.Null);
        }
        [Test]
        public void GetByUserId_NonZeroId()
        {
            mockCartRepo.Setup(x => x.GetAllOrdersByUserId(1)).Returns(_userOrderDetails);
            var result = _cartController.GetByUserId(1) as ObjectResult;
            Assert.That(result.Value, Is.Not.Null);
        }

        [Test]
        public void CreateOrder_NoFoodAdded()
        {
            UserOrderDetails userOrderDetails = new UserOrderDetails() {OrderId=3,UserId=3, FoodItemList =new List<FoodItemDetails>() { } };
            var result = _cartController.CreateOrder(userOrderDetails);
            Assert.That(result.Value,Is.Null);
        }
        [Test]
        public void CreateOrder_FoodAdded()
        {
            
            var result = _cartController.CreateOrder(_userOrderDetails[0]);
            Assert.That(result.Value, Is.Null);
        }
        [Test]
        public void TotalValueCalculation_nullOrder()
        {
            var result = _cartController.TotalValue(null) as ObjectResult;
            Assert.That(StatusCodeResult.Equals(result.StatusCode,404));
        }
        [Test]
        public void TotalValueCalculation_notNullOrder()
        {
            mockCartRepo.Setup(x => x.CalculateTotalValueAsync(_userOrderDetails[0]).Result).Returns(new TotalValue() { Value=0});
            var result = _cartController.TotalValue(_userOrderDetails[0]) as ObjectResult;
            Assert.That(StatusCodeResult.Equals(result.StatusCode, 200));
        }
       

    }
}