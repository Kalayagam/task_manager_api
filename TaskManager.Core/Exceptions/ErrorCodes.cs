using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.Core.Exceptions
{
    public class ErrorCodes
    {
        public const int TaskNotFoundResponse = 1000;
        public const int TaskBadRequestResponse = 1001;
        public const int TaskInternalServerResponse = 1002;
    }
}
