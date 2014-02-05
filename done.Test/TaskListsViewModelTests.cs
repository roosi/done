using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using done.Shared.Services;
using done.Shared.ViewModels;
using Google.Apis.Tasks.v1.Data;
using System.Collections.Generic;

namespace done.Test
{
    [TestClass]
    public class TaskListsViewModelTests
    {
        [TestMethod]
        public void TestGetTaskListsCommand()
        {
            var data = new DataService();
            var navi = new NavigationService();
            var dialog = new DialogService();
            var vm = new TaskListsViewModel(data, navi, dialog);
            
            vm.GetTaskListsCommand.Execute(null);

            Assert.AreEqual(3, vm.TaskLists.Count);
            Assert.AreEqual("1", vm.SelectedTaskList.Id);
            Assert.AreEqual(10, vm.SelectedTaskList.Tasks.Count);
        }

        public class DataService : IDataService
        {
            public void SetUserCredential(Google.Apis.Auth.OAuth2.UserCredential credential)
            {
                throw new NotImplementedException();
            }

            public async System.Threading.Tasks.Task<Google.Apis.Tasks.v1.Data.TaskLists> GetTaskListsAsync()
            {
                return new TaskLists() 
                    { 
                        Items = new List<TaskList>() 
                        { 
                            new TaskList() { Id = "1", Title = "My private" }, 
                            new TaskList() { Id = "2", Title = "My work" }, 
                            new TaskList() { Id = "3", Title = "Misc" } 
                        } 
                    };
            }

            public System.Threading.Tasks.Task<Google.Apis.Tasks.v1.Data.TaskList> CreateTaskListAsync(string title)
            {
                throw new NotImplementedException();
            }

            public System.Threading.Tasks.Task<string> DeleteTaskListAsync(string id)
            {
                throw new NotImplementedException();
            }

            public async System.Threading.Tasks.Task<Google.Apis.Tasks.v1.Data.Tasks> GetTasksAsync(string listId)
            {
                return new Tasks()
                {
                    Items = new List<Task>() { 
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
                    }
                };
            }

            public System.Threading.Tasks.Task<Google.Apis.Tasks.v1.Data.Task> CreateTaskAsync(Google.Apis.Tasks.v1.Data.Task task, string listId)
            {
                throw new NotImplementedException();
            }

            public System.Threading.Tasks.Task<Google.Apis.Tasks.v1.Data.Task> UpdateTaskAsync(Google.Apis.Tasks.v1.Data.Task task, string listId)
            {
                throw new NotImplementedException();
            }

            public System.Threading.Tasks.Task<Google.Apis.Tasks.v1.Data.Task> MoveTaskAsync(Google.Apis.Tasks.v1.Data.Task task, Google.Apis.Tasks.v1.Data.Task previousTask, string listId)
            {
                throw new NotImplementedException();
            }

            public System.Threading.Tasks.Task<string> DeleteTaskAsync(Google.Apis.Tasks.v1.Data.Task task, string listId)
            {
                throw new NotImplementedException();
            }
        }

        public class DialogService : IDialogService
        {
            public MessageResult ShowMessage(string message, string caption, MessageButton button)
            {
                throw new NotImplementedException();
            }

            public System.Threading.Tasks.Task<MessageResult> ShowMessageAsync(string message, string caption, MessageButton button)
            {
                throw new NotImplementedException();
            }
        }

        public class NavigationService : INavigationService
        {
            public void Navigate(Type viewModelType)
            {
                throw new NotImplementedException();
            }

            public void GoBack()
            {
                throw new NotImplementedException();
            }

            public bool CanGoBack()
            {
                throw new NotImplementedException();
            }
        }
    }
}
