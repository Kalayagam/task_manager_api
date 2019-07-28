using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.Core.Exceptions
{
    public class TaskManagerException : Exception
    {
        public int ErrorNumber { get; set; }
        public TaskManagerException()
        {

        }

        public TaskManagerException(int errorNumber, string message) : base(message)
        {
            ErrorNumber = errorNumber;
        }
    }
}
