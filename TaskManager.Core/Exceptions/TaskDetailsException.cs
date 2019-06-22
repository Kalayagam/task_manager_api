using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.Core.Exceptions
{
    public class TaskDetailsException : Exception
    {
        public int ErrorNumber { get; set; }
        public TaskDetailsException()
        {

        }

        public TaskDetailsException(int errorNumber, string message) : base(message)
        {
            ErrorNumber = errorNumber;
        }
    }
}
