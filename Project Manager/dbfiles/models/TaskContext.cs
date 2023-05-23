using System.Data.Entity;

namespace Project_Manager.dbfiles.models
{
   public class TaskContext : DbContext
    {
        public TaskContext() : base("DefaultConnection")
        {

        }
        public DbSet<Task> tasks { get; set; }
    }
}