using JiraBoard_api.Modals;
using Microsoft.EntityFrameworkCore;

namespace JiraBoard_api.Context
{
    public class DataContext : DbContext
    {
        public DbSet<projectData> projects { get; set; }
    }
}
