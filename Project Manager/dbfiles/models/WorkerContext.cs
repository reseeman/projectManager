using System.Data.Entity;

namespace Project_Manager.dbfiles.models
{
   public class WorkerContext : DbContext
    {
        public WorkerContext() : base("DefaultConnection")
        {

        }
        public DbSet<Worker> workers { get; set; }
    }
}