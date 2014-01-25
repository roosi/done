using done.Shared.Services;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Tasks.v1;
using Google.Apis.Tasks.v1.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace done.Shared.Model
{
    public class DataService : IDataService
    {
        private const String _application = "done";
        private TasksService _service = null;

        public DataService()
        { 

        }

        public void SetUserCredential(UserCredential credential)
        {
            _service = new TasksService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = _application
            });
        }

        public async System.Threading.Tasks.Task<TaskLists> GetTaskListsAsync()
        {
            if (_service == null)
            {
                throw new Exception("Service is not initialized");
            }
            return await _service.Tasklists.List().ExecuteAsync();
        }


        public async System.Threading.Tasks.Task<Tasks> GetTasksAsync(string listId)
        {
            if (_service == null)
            {
                throw new Exception("Service is not initialized");
            }
            return await _service.Tasks.List(listId).ExecuteAsync();
        }


        public async System.Threading.Tasks.Task<Task> CreateTaskAsync(Task task, string listId)
        {
            if (_service == null)
            {
                throw new Exception("Service is not initialized");
            }
            return await _service.Tasks.Insert(task, listId).ExecuteAsync();
        }


        public async System.Threading.Tasks.Task<Task> UpdateTaskAsync(Task task, string listId)
        {
            if (_service == null)
            {
                throw new Exception("Service is not initialized");
            }
            if (task.Status.Equals("needsAction"))
            {
                task.Completed = null;
            }

            return await _service.Tasks.Update(task, listId, task.Id).ExecuteAsync();
        }

        public async System.Threading.Tasks.Task<string> DeleteTaskAsync(Task task, string listId)
        {
            if (_service == null)
            {
                throw new Exception("Service is not initialized");
            }

            return await _service.Tasks.Delete(listId, task.Id).ExecuteAsync();
        }
    }
}
