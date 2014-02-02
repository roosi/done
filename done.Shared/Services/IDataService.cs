using Google.Apis.Auth.OAuth2;
using Google.Apis.Tasks.v1.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace done.Shared.Services
{
    public interface IDataService
    {
        void SetUserCredential(UserCredential credential);

        Task<TaskLists> GetTaskListsAsync();
        Task<TaskList> CreateTaskListAsync(string title);
        Task<string> DeleteTaskListAsync(string id);

        Task<Tasks> GetTasksAsync(string listId);

        Task<Google.Apis.Tasks.v1.Data.Task> CreateTaskAsync(Google.Apis.Tasks.v1.Data.Task task, string listId);
        Task<Google.Apis.Tasks.v1.Data.Task> UpdateTaskAsync(Google.Apis.Tasks.v1.Data.Task task, string listId);
        Task<Google.Apis.Tasks.v1.Data.Task> MoveTaskAsync(Google.Apis.Tasks.v1.Data.Task task, Google.Apis.Tasks.v1.Data.Task previousTask, string listId);

        Task<string> DeleteTaskAsync(Google.Apis.Tasks.v1.Data.Task task, string listId);
    }
}
