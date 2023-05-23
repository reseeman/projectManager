using System;
using System.ComponentModel.DataAnnotations;

namespace Project_Manager.dbfiles.models
{
    public class Project
    {
        [Key]
        public int idProject { get; set; }

        public string nameProject { get; set; }

        public DateTime dateStart { get; set; }

        public DateTime dateFinish { get; set; }

        public int projectPeriodDays { get; set; }

        public bool isProjectReady { get; set; }

        public string description { get; set; }
    }
}
