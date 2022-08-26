using System.ComponentModel.DataAnnotations;

namespace FoodPrice.Entity.Model
{
    public class FoodItem
    {
        [Key]
        public int FoodId { get; set; }

        public string FoodName { get; set; }

        public double FoodPrice { get; set; }
    }
}
