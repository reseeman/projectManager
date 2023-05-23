using System.Data.Entity;

namespace Project_Manager.dbfiles.models
{
    public class ProjectContext : DbContext
    {
        public ProjectContext() : base("DefaultConnection")
        {

        }
        public DbSet<Project> projects { get; set; }
    }
}