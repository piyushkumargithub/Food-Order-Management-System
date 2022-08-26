using CalculateCartMicroservice.Entity.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CalculateCartMicroservice.Repository
{
    public interface ICartRepo
    {   
        public Task<TotalValue> CalculateTotalValueAsync(UserOrderDetails userOrderDetails);
        public UserOrderDetails GetUserOrderDetailsByOrderId(long id);
        public List<UserOrderDetails> GetAllOrdersByUserId(int id);
        public string AddOrderDetails(UserOrderDetails userOrder);
        
    }
}
