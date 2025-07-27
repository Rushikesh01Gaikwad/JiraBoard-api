using JiraBoard_api.Modals;
using Microsoft.EntityFrameworkCore;

namespace JiraBoard_api.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<projectData> projects { get; set; }
        public DbSet<User> users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<projectData>()
                .HasOne(p => p.user)
                .WithMany(u => u.ProjectData)
                .HasForeignKey(p => p.userId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
