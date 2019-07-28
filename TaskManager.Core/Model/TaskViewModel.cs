using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TaskManager.Core
{
    public class TaskViewModel
    {
        public int Id { get; set; }
        public int ParentId { get; set; }

        public string TaskName { get; set; }

        public string ParentTaskName { get; set; }

        public bool IsParentTask { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int Priority { get; set; }

        public int ProjectId { get; set; }

        [Range(0,1)]
        public int Status { get; set; }


    }
}
