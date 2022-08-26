using System.ComponentModel.DataAnnotations;

namespace CalculateCartMicroservice.Entity.Model
{
    public class FoodItemDetails
    {
        [Key]
        public int FoodId { get; set; }
        public string FoodName { get; set; }
        public int Quantity { get; set; }
    }
}
