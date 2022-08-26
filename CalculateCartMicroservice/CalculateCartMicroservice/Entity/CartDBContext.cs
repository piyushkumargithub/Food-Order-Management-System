using CalculateCartMicroservice.Entity.Model;
using Microsoft.EntityFrameworkCore;


namespace CalculateCartMicroservice.Entity
{
    public class CartDBContext:DbContext
    {
        public CartDBContext(DbContextOptions<CartDBContext> options) : base(options)
        {

        }
        public virtual DbSet<UserOrderDetails> UserOrderDetails { get; set; }
        public virtual DbSet<FoodItemDetails> FoodItemDetails { get; set; }
        
    }
}
