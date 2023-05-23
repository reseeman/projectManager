using System;
using System.ComponentModel.DataAnnotations;

namespace Project_Manager.dbfiles.models
{
    public class Task
    {
        [Key]
        public int idTask { get; set; }

        public string nameTask { get; set; }

        public DateTime taskStart { get; set; }

        public DateTime taskFinish { get; set; }

        public int taskPeriodDays { get; set; }

        public int idProject { get; set; }

        public int idWorker { get; set; }
    }
}
