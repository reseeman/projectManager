using System.ComponentModel.DataAnnotations;

namespace Project_Manager.dbfiles.models
{
    public class Worker
    {
        [Key]
        public int idWorker { get; set; }

        public string nameWorker { get; set; }
    }
}
