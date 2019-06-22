using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.Core
{
    public class TaskViewModel
    {
        public int Id { get; set; }
        public int ParentId { get; set; }

        public string TaskName { get; set; }

        public string ParentTaskName { get; set; }
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int Priority { get; set; }


    }
}
