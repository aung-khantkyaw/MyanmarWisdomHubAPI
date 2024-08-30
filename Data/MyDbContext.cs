using Microsoft.EntityFrameworkCore;
using MyanmarWisdomHubAPI.Models;

namespace MyanmarWisdomHubAPI.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Proverb> Proverb { get; set; } = default!;
        public DbSet<MyanmarWisdomHubAPI.Models.Riddle> Riddle { get; set; } = default!;
    }
}
