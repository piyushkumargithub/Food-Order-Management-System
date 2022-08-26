using AuthMicroservice.Entity.Model;

namespace AuthMicroservice.Repository
{
    public interface IAuthRepo
    {
        public string RegisterUser(DTOUser dTOUser);
        public TokenObject LoginUser(DTOUser dTOUser);
        
    }
}
