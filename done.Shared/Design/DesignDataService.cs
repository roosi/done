using done.Shared.Services;
using Google.Apis.Tasks.v1.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace done.Shared.Design
{
    public class DesignDataService : IDataService
    {
        public void SetUserCredential(Google.Apis.Auth.OAuth2.UserCredential credential)
        {
            throw new NotImplementedException();
        }

        public async System.Threading.Tasks.Task<TaskLists> GetTaskListsAsync()
        {
            return new TaskLists() { Items = new List<TaskList>() { new TaskList() { Title = "My private" }, new TaskList() { Title = "My work" }, new TaskList() { Title = "Misc" } } };
        }

        public async System.Threading.Tasks.Task<Tasks> GetTasksAsync(string listId)
        {
            return new Tasks() { Items = new List<Task>() { 
                new Task() { Title = "My Private Task 1", Status = "needsAction", Due = DateTime.Today.AddDays(9),
                    Notes = "Lorem ipsum dolor sit amet, consectetur adipiscing elit." }, 
                new Task() { Title = "My Private Task 2", Status = "needsAction", Due = DateTime.Today.AddDays(8),
                    Notes = "Aenean nunc massa, condimentum quis tellus tempor, luctus porttitor lectus. Fusce at varius turpis. Integer laoreet est consectetur lobortis faucibus. Sed metus libero, aliquam id posuere non, tempus sit amet nisl. Etiam non dolor adipiscing, suscipit quam ut, ultricies arcu. Etiam justo mauris, posuere sit amet risus nec, volutpat molestie sapien. Ut nec urna et diam mattis cursus. Integer lectus nisl, luctus eu adipiscing in, vehicula in purus. Sed laoreet nulla mi, a rhoncus ipsum ultricies eu. Cras laoreet pretium est a fermentum." },
                new Task() { Title = "My Private Task 3", Status = "needsAction", Due = DateTime.Today.AddDays(7),
                    Notes = "Donec eu lectus nec arcu lacinia scelerisque id ac sapien." },
                new Task() { Title = "My Private Task 4", Status = "needsAction", Due = DateTime.Today.AddDays(6),
                    Notes = "Nam vitae mauris scelerisque quam condimentum auctor sollicitudin vitae odio." },
                new Task() { Title = "My Private Task 5", Status = "needsAction", Due = DateTime.Today.AddDays(5),
                    Notes = "Nam venenatis turpis id mattis eleifend." },
                new Task() { Title = "My Private Task 6", Status = "needsAction", Due = DateTime.Today.AddDays(1),
                    Notes = "Phasellus eu massa ac nulla ultrices faucibus euismod vel mi." },
                new Task() { Title = "My Private Task 7", Status = "needsAction", Due = DateTime.Today,
                    Notes = "Phasellus posuere purus eget commodo feugiat." },
                new Task() { Title = "My Private Task 8", Status = "completed", Due = DateTime.Today.AddDays(-2),
                    Notes = "Nulla condimentum leo eu scelerisque aliquam." },
                new Task() { Title = "My Private Task 9", Status = "completed", Due = DateTime.Today.AddDays(-3),
                    Notes = "Maecenas tristique dui condimentum libero feugiat, ut ullamcorper sem porttitor."},
                new Task() { Title = "My Private Task 10", Status = "completed", Due = DateTime.Today.AddDays(-4),
                    Notes = "Mauris consectetur lectus vel tellus hendrerit tempor." }
            } };
        }


        public System.Threading.Tasks.Task<Task> CreateTaskAsync(Task task, string listId)
        {
            throw new NotImplementedException();
        }


        public System.Threading.Tasks.Task<Task> UpdateTaskAsync(Task task, string listId)
        {
            throw new NotImplementedException();
        }


        public System.Threading.Tasks.Task<string> DeleteTaskAsync(Task task, string listId)
        {
            throw new NotImplementedException();
        }


        public System.Threading.Tasks.Task<TaskList> CreateTaskListAsync(string title)
        {
            throw new NotImplementedException();
        }


        public System.Threading.Tasks.Task<string> DeleteTaskListAsync(string id)
        {
            throw new NotImplementedException();
        }


        public System.Threading.Tasks.Task<Task> MoveTaskAsync(Task task, Task previousTask, string listId)
        {
            throw new NotImplementedException();
        }
    }
}
