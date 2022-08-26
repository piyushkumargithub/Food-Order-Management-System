using AuthMicroservice.Entity;
using AuthMicroservice.Entity.Model;
using AuthMicroservice.Logger;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace AuthMicroservice.Repository
{
    public class AuthRepo : IAuthRepo
    {
        private readonly ILoggerManager _logger;
        private readonly AuthDBContext _authDBContext;
        private readonly IConfiguration _configuration;
        public AuthRepo(IConfiguration configuration,AuthDBContext authDBContext,ILoggerManager logger)
        {
            _configuration = configuration;
            _authDBContext = authDBContext;
            _logger = logger;
        }

        public string RegisterUser(DTOUser dTOUser)
        {
            User obj = _authDBContext.Users.FirstOrDefault(c => c.UserName == dTOUser.UserName);
            if (obj != null)
            {
                _logger.LogInformation("A user with username already exist:"+dTOUser.UserName);
                return "A user with this username already exist";
            }
            User user = new User();
            user.UserName = dTOUser.UserName;
            CreatePasswordHash(dTOUser.Password,out byte[] passwordHash,out byte[] passwordSalt);
            user.PasswordHashed=passwordHash;
            user.PasswordSalt = passwordSalt;
            _authDBContext.Users.Add(user);
            _authDBContext.SaveChanges();

            _logger.LogInformation(nameof(RegisterUser)+" successfully registered user with Username:"+dTOUser.UserName);
            return "Successfully Registered";
        }

        public TokenObject LoginUser(DTOUser dTOUser)
        {
            try {
                User obj = _authDBContext.Users.FirstOrDefault(c => c.UserName == dTOUser.UserName);
                
                if (obj == null)
                {   
                    _logger.LogInformation(nameof(LoginUser)+" returned nothing for Username:"+dTOUser.UserName);
                    return null;
                }
                else if (VerifyPasswordHash(dTOUser.Password, obj.PasswordHashed, obj.PasswordSalt))
                {
                    TokenObject tokenObject = new TokenObject()
                    {
                        UserId = obj.Id,
                        UserName = obj.UserName,
                        TokenString= CreateToken(obj)
                    };
                    
                    return tokenObject;
                }
                else return  new TokenObject()
                {
                    UserId = obj.Id,
                    UserName = obj.UserName,
                    TokenString = "Incorrect Password"
                }; ;
            }
            catch(Exception ex) {
                _logger.LogInformation("Error at :"+nameof(LoginUser)+" exception :"+ex.Message);
                return new TokenObject()
                {
                    TokenString = "Exception caught"
                }; ;
            }
            
        }


        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
            
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
    }
}
