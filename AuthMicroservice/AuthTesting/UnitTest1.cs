using AuthMicroservice.Controllers;
using AuthMicroservice.Entity.Model;
using AuthMicroservice.Logger;
using AuthMicroservice.Repository;
using Moq;
using NUnit.Framework;

namespace AuthTesting
{
    public class Tests
    {
        private readonly Mock<ILoggerManager> mockLogger = new Mock<ILoggerManager>();
        private readonly Mock<IAuthRepo> mockRepo = new Mock<IAuthRepo>();
        public AuthController authController;
        public DTOUser dTOUSer;
        
        public Tests()
        {
            authController = new AuthController( mockRepo.Object,mockLogger.Object);
        }
        
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void Register_withno_UserName()
        {
            dTOUSer = new DTOUser() { UserName=""};
            var result=authController.Register(dTOUSer).Result.Result;
            Assert.That(result.ToString ,Is.EqualTo("Microsoft.AspNetCore.Mvc.BadRequestObjectResult"));
        }
        [Test]
        public void Register_withno_Password()
        {
            dTOUSer = new DTOUser() { UserName = "user",Password="" };
            var result = authController.Register(dTOUSer).Result.Result;
            Assert.That(result.ToString, Is.EqualTo("Microsoft.AspNetCore.Mvc.BadRequestObjectResult"));
        }
        [Test]
        public void Register_with_both()
        {
            dTOUSer = new DTOUser { Password = "password", UserName = "user" };
            mockRepo.Setup(x => x.RegisterUser(dTOUSer)).Returns(() => "ok");
            var result=authController.Register(dTOUSer);
            Assert.That(result.Result.Result.ToString, Is.EqualTo("Microsoft.AspNetCore.Mvc.OkObjectResult"));
        }
        [Test]
        public void Login_withno_UserName()
        {
            dTOUSer = new DTOUser() { UserName = "" };
            var result = authController.Login(dTOUSer).Result.Result;
            Assert.That(result.ToString, Is.EqualTo("Microsoft.AspNetCore.Mvc.BadRequestObjectResult"));
        }
        [Test]
        public void Login_withno_Password()
        {
            dTOUSer = new DTOUser() { UserName = "user", Password = "" };
            var result = authController.Login(dTOUSer).Result.Result;
            Assert.That(result.ToString, Is.EqualTo("Microsoft.AspNetCore.Mvc.BadRequestObjectResult"));
        }
        [Test]
        public void Login_with_both()
        {
            dTOUSer = new DTOUser { Password = "password", UserName = "user" };
            mockRepo.Setup(x => x.LoginUser(dTOUSer)).Returns(new TokenObject());
            var result = authController.Register(dTOUSer);
            Assert.That(result.Result.Result.ToString, Is.EqualTo("Microsoft.AspNetCore.Mvc.OkObjectResult"));
        }
    }
}