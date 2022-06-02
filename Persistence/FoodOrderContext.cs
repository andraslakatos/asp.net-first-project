using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class FoodOrderContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<Item> Items { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;

        public FoodOrderContext(DbContextOptions<FoodOrderContext> options) : base(options)
        {
        }
    }
}
