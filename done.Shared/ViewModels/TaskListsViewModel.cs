using done.Shared.Services;
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
    public class TaskListsViewModel : ViewModelExtBase
    {
        /// <summary>
        /// Initializes a new instance of the TaskListsViewModel class.
        /// </summary>
        public TaskListsViewModel(IDataService dataService, INavigationService navigationService)
            : base(dataService, navigationService)
        {
#if DEBUG
            if (IsInDesignMode)
            {
                ExecuteGetTaskListsCommand();
            }
#endif
        }

        /// <summary>
        /// The <see cref="TaskLists" /> property's name.
        /// </summary>
        public const string TaskListsPropertyName = "TaskLists";

        private ObservableCollection<TaskListViewModel> _taskLists = new ObservableCollection<TaskListViewModel>();

        /// <summary>
        /// Sets and gets the TaskLists property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ObservableCollection<TaskListViewModel> TaskLists
        {
            get
            {
                return _taskLists;
            }
            set
            {
                Set(TaskListsPropertyName, ref _taskLists, value);
            }
        }

        /// <summary>
        /// The <see cref="SelectedTaskList" /> property's name.
        /// </summary>
        public const string SelectedTaskListPropertyName = "SelectedTaskList";

        private TaskListViewModel _selectedTaskList = null;

        /// <summary>
        /// Sets and gets the SelectedTaskList property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public TaskListViewModel SelectedTaskList
        {
            get
            {
                return _selectedTaskList;
            }
            set
            {
                if (Set(SelectedTaskListPropertyName, ref _selectedTaskList, value))
                {
                    if (value != null)
                    {
                        _selectedTaskList.GetTasksCommand.Execute(null);
                    }
                }
            }
        }

        private RelayCommand _getTaskListsCommand;

        /// <summary>
        /// Gets the GetTaskListsCommand.
        /// </summary>
        public RelayCommand GetTaskListsCommand
        {
            get
            {
                return _getTaskListsCommand ?? (_getTaskListsCommand = new RelayCommand(
                    ExecuteGetTaskListsCommand,
                    CanExecuteGetTaskListsCommand));
            }
        }

        private async void ExecuteGetTaskListsCommand()
        {
            IsLoading = true;
            var result = await _dataService.GetTaskListsAsync();

            _taskLists.Clear();
            foreach (TaskList list in result.Items)
            {
                TaskLists.Add(new TaskListViewModel(list, _dataService, _navigationService));
            }
            if (_taskLists.Count > 0)
            {
                SelectedTaskList = _taskLists[0];
            }

            IsLoading = false;
            _getTaskListsCommand.RaiseCanExecuteChanged();
        }

        private bool CanExecuteGetTaskListsCommand()
        {
            return IsLoading == false;
        }
    }
}