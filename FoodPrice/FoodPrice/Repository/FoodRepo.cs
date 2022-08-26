using FoodPrice.Entity;
using FoodPrice.Entity.Model;
using System.Linq;

namespace FoodPrice.Repository
{
    public class FoodRepo : IFoodRepo
    {
        private readonly FoodDBContext _foodDBContext;
        public FoodRepo(FoodDBContext foodDBContext)
        {
            _foodDBContext=foodDBContext;
        }
        public FoodItem GetFoodByName(string foodName)
        {
            var food = _foodDBContext.FoodItems.FirstOrDefault(a => a.FoodName.ToLower() == foodName.ToLower());
            if (food == null)
            {
                return null;
            }
            return food;
        }
    }
}
