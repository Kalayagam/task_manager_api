using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.Repository.Context
{
    public class User
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int EmployeeId { get; set; }

        public Project Project { get; set; }

        public TaskDetails TaskDetails { get; set; }
    }
}
