using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.Repository.Context
{
    public class Project
    {
        public int Id { get; set; }

        public string ProjectName { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int Priority { get; set; }
    }
}
