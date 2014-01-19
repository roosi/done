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
        Task<Tasks> GetTasksAsync(string listId);
    }
}
