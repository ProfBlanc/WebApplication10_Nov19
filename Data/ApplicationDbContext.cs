using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplication10_Nov19.Models;

namespace WebApplication10_Nov19.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<PurchaseModel> Purchases { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
