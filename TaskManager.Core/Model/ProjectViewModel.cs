using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.Core.Model
{
    public class ProjectViewModel
    {
        public int Id { get; set; }

        public string ProjectName { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public int Priority { get; set; }

        public int UserId { get; set; }

        public int TotalNumberOfTasks { get; set; }

        public int NumberOfTasksCompleted { get; set; }
    }
}
