using Microsoft.EntityFrameworkCore;
using MyanmarWisdomHubAPI.Models;

namespace MyanmarWisdomHubAPI.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }

        public DbSet<MyanmarWisdomHubAPI.Models.User.User> Users { get; set; } = default!;
        public DbSet<MyanmarWisdomHubAPI.Models.Proverb> Proverb { get; set; } = default!;
        public DbSet<MyanmarWisdomHubAPI.Models.Riddle> Riddle { get; set; } = default!;
        public DbSet<MyanmarWisdomHubAPI.Models.Post> Post { get; set; } = default!;
        public DbSet<MyanmarWisdomHubAPI.Models.Quiz> Quiz { get; set; } = default!;
        public DbSet<MyanmarWisdomHubAPI.Models.Attempt_Quiz> Attempt_Quiz { get; set; } = default!;
        public DbSet<MyanmarWisdomHubAPI.Models.Notification> Notification { get; set; } = default!;
    }
}
