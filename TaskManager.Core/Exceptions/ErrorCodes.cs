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

        public const int ProjectNotFoundResponse = 1000;
        public const int ProjectBadRequestResponse = 1001;
        public const int ProjectInternalServerResponse = 1002;
    }
}
