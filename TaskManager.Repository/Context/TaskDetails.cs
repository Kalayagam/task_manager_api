using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.Repository.Context
{
    public class TaskDetails
    {
        public int Id { get; set; }       

        public string TaskName { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int Priority { get; set; }

        public ParentTask ParentTask { get; set; }

        public TaskStatus Status { get; set; }

        public Project Project { get; set; }

        public User User { get; set; }
    }

    public enum TaskStatus
    {
        InProgress,
        Complete
    }
}
