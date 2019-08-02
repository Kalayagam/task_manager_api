using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Core;

namespace TaskManager.Business.Interfaces
{
    public interface ITaskBusiness : IBusiness<TaskViewModel>
    {
        Task<IEnumerable<TaskViewModel>> GetAllParent();
    }
}
