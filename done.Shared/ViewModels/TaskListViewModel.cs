using done.Shared.Messages;
using done.Shared.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Google.Apis.Tasks.v1.Data;
using System.Collections.ObjectModel;

namespace done.Shared.ViewModels
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class TaskListViewModel : ViewModelExtBase
    {
        private TaskList _model;

        /// <summary>
        /// Initializes a new instance of the TaskListViewModel class.
        /// </summary>
        public TaskListViewModel(TaskList model, IDataService dataService, INavigationService navigationService)
            : base(dataService, navigationService)
        {
            _model = model;

            MessengerInstance.Register<TaskDeletedMessage>(this, message =>
                {
                    Tasks.Remove(message.Content);
                });
        }

        public string Title 
        { 
            get 
            {
                return _model.Title;
            } 
        }

        /// <summary>
        /// The <see cref="SelectedTask" /> property's name.
        /// </summary>
        public const string SelectedTaskPropertyName = "SelectedTask";

        private TaskViewModel _selectedTask = null;

        /// <summary>
        /// Sets and gets the SelectedTask property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public TaskViewModel SelectedTask
        {
            get
            {
                return _selectedTask;
            }
            set
            {
                if (Set(SelectedTaskPropertyName, ref _selectedTask, value))
                {
                    if (value != null)
                    {
                        _navigationService.Navigate(typeof(TaskViewModel));
                    }
                }
            }
        }

        /// <summary>
        /// The <see cref="Tasks" /> property's name.
        /// </summary>
        public const string TasksPropertyName = "Tasks";

        private ObservableCollection<TaskViewModel> _tasks = new ObservableCollection<TaskViewModel>();

        /// <summary>
        /// Sets and gets the Tasks property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ObservableCollection<TaskViewModel> Tasks
        {
            get
            {
                return _tasks;
            }
            set
            {
                Set(TasksPropertyName, ref _tasks, value);
            }
        }

        private RelayCommand _getTasksCommand;

        /// <summary>
        /// Gets the GetTaskCommand.
        /// </summary>
        public RelayCommand GetTasksCommand
        {
            get
            {
                return _getTasksCommand ?? (_getTasksCommand = new RelayCommand(
                    ExecuteGetTasksCommand,
                    CanExecuteGetTasksCommand));
            }
        }

        private async void ExecuteGetTasksCommand()
        {
            IsLoading = true;

            var result = await _dataService.GetTasksAsync(_model.Id);

            _tasks.Clear();
            foreach (Task task in result.Items)
            {
                Tasks.Add(new TaskViewModel(task, _model.Id ,_dataService, _navigationService));
            }
#if DEBUG
            if (IsInDesignMode)
            {
                if (_tasks.Count > 0)
                {
                    SelectedTask = _tasks[0];
                }
            }
#endif
            IsLoading = false;
        }

        private bool CanExecuteGetTasksCommand()
        {
            return IsLoading == false;
        }

        private RelayCommand _createTaskCommand;

        /// <summary>
        /// Gets the CreateTaskCommand.
        /// </summary>
        public RelayCommand CreateTaskCommand
        {
            get
            {
                return _createTaskCommand ?? (_createTaskCommand = new RelayCommand(
                    ExecuteCreateTaskCommand,
                    CanExecuteCreateTaskCommand));
            }
        }

        private async void ExecuteCreateTaskCommand()
        {
            Task task = new Task() { Title = string.Empty };

            IsLoading = true;
            task = await _dataService.CreateTaskAsync(task, _model.Id);
            IsLoading = false;

            TaskViewModel newTask = new TaskViewModel(task, _model.Id, _dataService, _navigationService);
            Tasks.Insert(0, newTask);
        }

        private bool CanExecuteCreateTaskCommand()
        {
            return IsLoading == false;
        }
    }
}