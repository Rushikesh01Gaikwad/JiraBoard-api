using JiraBoard_api.Modals;
using Microsoft.EntityFrameworkCore;

namespace JiraBoard_api.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }   
        public DbSet<projectData> projects { get; set; }
    }
}
