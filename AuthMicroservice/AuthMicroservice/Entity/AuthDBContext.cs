using AuthMicroservice.Entity.Model;
using Microsoft.EntityFrameworkCore;

namespace AuthMicroservice.Entity
{
    public class AuthDBContext:DbContext
    {
        public AuthDBContext(DbContextOptions<AuthDBContext> options) : base(options)
        {

        }
        public virtual DbSet<User> Users { get; set; }
    }
}
