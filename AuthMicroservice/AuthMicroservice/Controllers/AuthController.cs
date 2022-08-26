using AuthMicroservice.Entity.Model;
using AuthMicroservice.Logger;
using AuthMicroservice.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AuthMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IAuthRepo _authRepo;
        public AuthController(IAuthRepo authRepo,ILoggerManager loggerManager)
        {
            _authRepo = authRepo ;
            _logger = loggerManager;
        }

        // POST api/<AuthController>
        [HttpPost("register")]
        public async Task<ActionResult<string>> Register(DTOUser dTOUser)
        {
            if (dTOUser.UserName == null || dTOUser.UserName.Length == 0)
            {
                return BadRequest("Provide username");
            }
            if (dTOUser.Password == null || dTOUser.Password.Length==0)
            {
                return BadRequest("Provide password");
            }

            string result = _authRepo.RegisterUser(dTOUser);

            return Ok( result);
        }

        [HttpPost("login")]
        public async Task<ActionResult<TokenObject>> Login(DTOUser dTOUser)
        {
            if (dTOUser.UserName == null || dTOUser.UserName.Length == 0)
            {
                _logger.LogInformation(nameof(Login) + " no username provided");
                return BadRequest("Provide username");
            }
            if (dTOUser.Password == null || dTOUser.Password.Length == 0)
            {
                _logger.LogInformation(nameof(Login) + " no password provided");
                return BadRequest("Provide password");
            }
            TokenObject result = _authRepo.LoginUser(dTOUser);
            if (result == null)
            {
                _logger.LogInformation(nameof(Login) + " no such user exist :"+ dTOUser.UserName);
                return BadRequest("No such user exist");
            }
            
            else if(result.TokenString== "Exception caught")
            {
                _logger.LogInformation(nameof(Login) + " Exception Caught");
                return BadRequest("Exception caught");
            }
            else if (result.TokenString == "Incorrect Password")
            {
                _logger.LogInformation(nameof(Login) + " Incorrect Password attempt");
                return BadRequest("Incorrect Password");
            }

            return Ok(result);
        }


    }
}
