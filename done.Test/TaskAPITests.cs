using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Tasks.v1;
using System.Threading;
using Google.Apis.Services;
using Google.Apis.Tasks.v1.Data;

namespace done.Test
{
    [TestClass]
    public class TaskAPITests
    {
        private const String _clientId = "552948890600.apps.googleusercontent.com";
        private const String _clientSecret = "H_AA4PQQvKtmcHYubTd5SEHi";
        private const String _application = "done";

        [TestMethod]
        public async System.Threading.Tasks.Task TestGetTaskLists()
        {
            UserCredential credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                new ClientSecrets
                {
                    ClientId = _clientId,
                    ClientSecret = _clientSecret
                },
                new[] { TasksService.Scope.Tasks },
                "user",
                CancellationToken.None);

            var service = new TasksService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = _application
            });

            TaskLists lists = await service.Tasklists.List().ExecuteAsync();
            Assert.IsTrue(lists.Items.Count > 0);

            foreach (TaskList list in lists.Items)
            {
                Assert.IsNotNull(list.Title);
            }
        }

        [TestMethod]
        public async System.Threading.Tasks.Task TestCreateNewTask()
        {
            UserCredential credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                new ClientSecrets
                {
                    ClientId = _clientId,
                    ClientSecret = _clientSecret
                },
                new[] { TasksService.Scope.Tasks },
                "user",
                CancellationToken.None);

            var service = new TasksService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = _application
            });

            TaskLists lists = await service.Tasklists.List().ExecuteAsync();
            Assert.IsTrue(lists.Items.Count > 0);
            string listId = lists.Items[0].Id;

            Task newTask = new Task()
            {
                Title = "Test",   
            };

            Task result = await service.Tasks.Insert(newTask, listId).ExecuteAsync();
            Assert.AreEqual(newTask.Title, result.Title);
        }

        [TestMethod]
        public async System.Threading.Tasks.Task TestGetTasks()
        {
            UserCredential credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                new ClientSecrets
                {
                    ClientId = _clientId,
                    ClientSecret = _clientSecret
                },
                new[] { TasksService.Scope.Tasks },
                "user",
                CancellationToken.None);

            var service = new TasksService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = _application
            });

            TaskLists lists = await service.Tasklists.List().ExecuteAsync();
            Assert.IsTrue(lists.Items.Count > 0);
            string listId = lists.Items[0].Id;

            Tasks result = await service.Tasks.List(listId).ExecuteAsync();

            foreach (Task task in result.Items)
            {
                string title = task.Title;
            }
        }

        [TestMethod]
        public async System.Threading.Tasks.Task TestUpdateTask()
        {
            UserCredential credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                new ClientSecrets
                {
                    ClientId = _clientId,
                    ClientSecret = _clientSecret
                },
                new[] { TasksService.Scope.Tasks },
                "user",
                CancellationToken.None);

            var service = new TasksService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = _application
            });

            TaskLists lists = await service.Tasklists.List().ExecuteAsync();
            Assert.IsTrue(lists.Items.Count > 0);
            string listId = lists.Items[0].Id;

            Tasks result = await service.Tasks.List(listId).ExecuteAsync();

            Task task = result.Items[0];
            task.Title = "Updated test";

            Task updated = await service.Tasks.Update(task, listId, task.Id).ExecuteAsync();
            Assert.AreEqual(task.Title, updated.Title);
        }

    }
}
