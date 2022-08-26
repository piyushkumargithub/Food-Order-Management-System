using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CalculateCartMicroservice.Entity.Model
{
    public class UserOrderDetails
    {
        [Key]
        public long OrderId { get; set; }

        public int UserId { get; set; }
        
        public List<FoodItemDetails> FoodItemList { get; set; }
    }
}
