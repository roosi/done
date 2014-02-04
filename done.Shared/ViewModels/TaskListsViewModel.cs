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
        public TaskListsViewModel(IDataService dataService, INavigationService navigationService, IDialogService dialogService)
            : base(dataService, navigationService, dialogService)
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

            try
            {
                var result = await _dataService.GetTaskListsAsync();
                _taskLists.Clear();
                foreach (TaskList list in result.Items)
                {
                    TaskLists.Add(new TaskListViewModel(list, _dataService, _navigationService, _dialogService));
                }
            }
            catch (Google.GoogleApiException e)
            {
                _dialogService.ShowMessage(e.Message, e.ServiceName, MessageButton.OK);
            }


            if (_taskLists.Count > 0)
            {
                SelectedTaskList = _taskLists[0];
            }

            IsLoading = false;
            if (_getTaskListsCommand != null)
            {
                _getTaskListsCommand.RaiseCanExecuteChanged();
            }
            if (_deleteTaskListCommand != null)
            {
                _deleteTaskListCommand.RaiseCanExecuteChanged();
            }
            if (_createTaskListCommand != null)
            {
                _createTaskListCommand.RaiseCanExecuteChanged();
            }
        }

        private bool CanExecuteGetTaskListsCommand()
        {
            return IsLoading == false;
        }

        /// <summary>
        /// The <see cref="NewTaskListTitle" /> property's name.
        /// </summary>
        public const string NewTaskListTitlePropertyName = "NewTaskListTitle";

        private string _newTaskListTitle = string.Empty;

        /// <summary>
        /// Sets and gets the NewTaskListTitle property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string NewTaskListTitle
        {
            get
            {
                return _newTaskListTitle;
            }
            set
            {
                Set(NewTaskListTitlePropertyName, ref _newTaskListTitle, value);
            }
        }

        private RelayCommand _createTaskListCommand;

        /// <summary>
        /// Gets the CreateTaskListCommnad.
        /// </summary>
        public RelayCommand CreateTaskListCommand
        {
            get
            {
                return _createTaskListCommand ?? (_createTaskListCommand = new RelayCommand(
                    ExecuteCreateTaskListCommand,
                    CanExecuteCreateTaskListCommand));
            }
        }

        private async void ExecuteCreateTaskListCommand()
        {
            IsLoading = true;
            TaskList taskList = await _dataService.CreateTaskListAsync(NewTaskListTitle);
            IsLoading = false;
            TaskLists.Add(new TaskListViewModel(taskList, _dataService, _navigationService, _dialogService));
            SelectedTaskList = TaskLists[TaskLists.Count - 1];
            NewTaskListTitle = string.Empty;
        }

        private bool CanExecuteCreateTaskListCommand()
        {
            return IsLoading == false && string.IsNullOrEmpty(_newTaskListTitle) == false;
        }

        /// <summary>
        /// The <see cref="EditMode" /> property's name.
        /// </summary>
        public const string EditModePropertyName = "EditMode";

        private bool _editMode = false;

        /// <summary>
        /// Sets and gets the EditMode property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool EditMode
        {
            get
            {
                return _editMode;
            }
            set
            {
                if (Set(EditModePropertyName, ref _editMode, value))
                {
                    DeleteTaskListCommand.RaiseCanExecuteChanged();
                }
            }
        }

        private RelayCommand<TaskListViewModel> _deleteTaskListCommand;

        /// <summary>
        /// Gets the DeleteTaskListCommand.
        /// </summary>
        public RelayCommand<TaskListViewModel> DeleteTaskListCommand
        {
            get
            {
                return _deleteTaskListCommand ?? (_deleteTaskListCommand = new RelayCommand<TaskListViewModel>(
                    ExecuteDeleteTaskListCommand,
                    CanExecuteDeleteTaskListCommand));
            }
        }

        private async void ExecuteDeleteTaskListCommand(TaskListViewModel parameter)
        {
            MessageResult result = await _dialogService.ShowMessageAsync("Do you want to delete the task list permanently?", "Delete task list", MessageButton.OKCancel);
            if (result == MessageResult.OK)
            {
                IsLoading = true;
                string response = await _dataService.DeleteTaskListAsync(parameter.Id);
                IsLoading = false;

                if (string.IsNullOrEmpty(response))
                {

                    if (SelectedTaskList.Equals(parameter) && TaskLists.Count > 0)
                    {
                        int index = TaskLists.IndexOf(parameter);
                        TaskLists.Remove(parameter);
                        if (index > 0)
                        {
                            index--;
                        }
                        SelectedTaskList = TaskLists[index];
                    }
                    else
                    {
                        TaskLists.Remove(parameter);
                    }
                    
                }
                else
                {
                    await _dialogService.ShowMessageAsync(response, "Error", MessageButton.OK);
                }
            }
        }

        private bool CanExecuteDeleteTaskListCommand(TaskListViewModel parameter)
        {
            return _editMode == true && IsLoading == false;
        }
    }
}