using FoodPrice.Entity.Model;

namespace FoodPrice.Repository
{
    public interface IFoodRepo
    {
        FoodItem GetFoodByName(string foodName);
    }
}
