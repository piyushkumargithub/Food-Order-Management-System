using FoodPrice.Entity.Model;
using Microsoft.EntityFrameworkCore;

namespace FoodPrice.Entity
{
    public class FoodDBContext:DbContext
    {
        public FoodDBContext(DbContextOptions<FoodDBContext> options): base(options)
        {

        }
        public virtual DbSet<FoodItem> FoodItems { get; set; }
    }
}
