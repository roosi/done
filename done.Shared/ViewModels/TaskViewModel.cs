using done.Shared.Messages;
using done.Shared.Services;
using GalaSoft.MvvmLight.Command;
using Google.Apis.Tasks.v1.Data;
using System;
using System.Collections.ObjectModel;

namespace done.Shared.ViewModels
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class TaskViewModel : ViewModelExtBase
    {
        public const string StatusNew = "new";
        public const string StatusNeedsAction = "needsAction";
        public const string StatusCompleted = "completed";

        private Task _model;
        private string _listId;

        /// <summary>
        /// Initializes a new instance of the TaskViewModel class.
        /// </summary>
        public TaskViewModel(Task model, string listId, IDataService dataService, INavigationService navigationService, IDialogService dialogService)
            : base(dataService, navigationService, dialogService)
        {
            _model = model;
            _listId = listId;
            update();
        }

        private void update()
        {
            Title = _model.Title;
            Status = _model.Status;
            DueDate = _model.Due != null ? _model.Due.Value : DateTime.Today.ToLocalTime();
            Notes = _model.Notes;
        }

        private void updateModel()
        {
            _model.Title = _title;
            _model.Status = _status;
            _model.Due = _dueDate.ToLocalTime();
            _model.Notes = _notes;
        }

        /// <summary>
        /// The <see cref="IsEdited" /> property's name.
        /// </summary>
        public const string IsEditedPropertyName = "IsEdited";

        private bool _isEdited = false;

        /// <summary>
        /// Sets and gets the IsEdited property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool IsEdited
        {
            get
            {
                return _isEdited;
            }
            set
            {
                Set(IsEditedPropertyName, ref _isEdited, value);
            }
        }

        /// <summary>
        /// The <see cref="Title" /> property's name.
        /// </summary>
        public const string TitlePropertyName = "Title";

        private string _title = string.Empty;

        /// <summary>
        /// Sets and gets the Title property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                IsEdited = Set(TitlePropertyName, ref _title, value);
            }
        }

        /// <summary>
        /// The <see cref="IsCompleted" /> property's name.
        /// </summary>
        public const string IsCompletedPropertyName = "IsCompleted";

        private bool _isCompleted = false;

        /// <summary>
        /// Sets and gets the IsCompleted property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool IsCompleted
        {
            get
            {
                return _isCompleted;
            }
            set
            {
                Set(IsCompletedPropertyName, ref _isCompleted, value);
            }
        }

        /// <summary>
        /// The <see cref="Status" /> property's name.
        /// </summary>
        public const string StatusPropertyName = "Status";

        private string _status = StatusNew;

        /// <summary>
        /// Sets and gets the Status property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string Status
        {
            get
            {
                return _status;
            }
            set
            {
                if (IsEdited = Set(StatusPropertyName, ref _status, value))
                {
                    if (_status != null)
                    {
                        IsCompleted = _status.Equals(StatusCompleted);
                    }
                    RaisePropertyChanged(StatusInformationPropertyName);
                }
            }
        }

        /// <summary>
        /// The <see cref="Statuses" /> property's name.
        /// </summary>
        public const string StatusesPropertyName = "Statuses";

        private ObservableCollection<string> _statuses = new ObservableCollection<string> { StatusNew, StatusNeedsAction, StatusCompleted };

        /// <summary>
        /// Sets and gets the Statuses property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ObservableCollection<string> Statuses
        {
            get
            {
                return _statuses;
            }
            set
            {
                Set(StatusesPropertyName, ref _statuses, value);
            }
        }

        /// <summary>
        /// The <see cref="DueDate" /> property's name.
        /// </summary>
        public const string DueDatePropertyName = "DueDate";

        private DateTime _dueDate = DateTime.Today;

        /// <summary>
        /// Sets and gets the DueDate property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public DateTime DueDate
        {
            get
            {
                return _dueDate;
            }
            set
            {
                if (IsEdited = Set(DueDatePropertyName, ref _dueDate, value))
                {
                    RaisePropertyChanged(StatusInformationPropertyName);
                }
            }
        }

        public class StatusInfo
        {
            public string Status;
            public DateTime DueDate;
        }

        /// <summary>
        /// The <see cref="StatusInformation" /> property's name.
        /// </summary>
        public const string StatusInformationPropertyName = "StatusInformation";

        /// <summary>
        /// Gets the StatusInfo property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public StatusInfo StatusInformation
        {
            get
            {
                return new StatusInfo() { DueDate = _dueDate, Status = _status };
            }
        }

        /// <summary>
        /// The <see cref="Notes" /> property's name.
        /// </summary>
        public const string NotesPropertyName = "Notes";

        private string _notes = string.Empty;

        /// <summary>
        /// Sets and gets the Notes property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string Notes
        {
            get
            {
                return _notes;
            }
            set
            {
                IsEdited = Set(NotesPropertyName, ref _notes, value);
            }
        }

        private RelayCommand _updateTaskCommand;

        /// <summary>
        /// Gets the UpdateTaskCommand.
        /// </summary>
        public RelayCommand UpdateTaskCommand
        {
            get
            {
                return _updateTaskCommand ?? (_updateTaskCommand = new RelayCommand(
                    ExecuteUpdateTaskCommand,
                    CanExecuteUpdateTaskCommand));
            }
        }

        private async void ExecuteUpdateTaskCommand()
        {
            IsLoading = true;

            updateModel();
            _model = await _dataService.UpdateTaskAsync(_model, _listId);
            update();

            IsLoading = false;
            IsEdited = false;
            MessengerInstance.Send<TaskUpdatedMessage>(new TaskUpdatedMessage(this));
        }

        private bool CanExecuteUpdateTaskCommand()
        {
            return IsEdited && IsCompleted == false;
        }

        private RelayCommand _completeTaskCommand;

        /// <summary>
        /// Gets the CompleteTaskCommamd.
        /// </summary>
        public RelayCommand CompleteTaskCommamd
        {
            get
            {
                return _completeTaskCommand ?? (_completeTaskCommand = new RelayCommand(
                    ExecuteCompleteTaskCommamd,
                    CanExecuteCompleteTaskCommamd));
            }
        }

        private void ExecuteCompleteTaskCommamd()
        {
            Status = StatusCompleted;
            ExecuteUpdateTaskCommand();
        }

        private bool CanExecuteCompleteTaskCommamd()
        {
            return IsCompleted == false;
        }

        private RelayCommand _deleteTaskCommand;

        /// <summary>
        /// Gets the DeleteTaskCommand.
        /// </summary>
        public RelayCommand DeleteTaskCommand
        {
            get
            {
                return _deleteTaskCommand ?? (_deleteTaskCommand = new RelayCommand(
                    ExecuteDeleteTaskCommand,
                    CanExecuteDeleteTaskCommand));
            }
        }

        private async void ExecuteDeleteTaskCommand()
        {
            MessageResult result = await _dialogService.ShowMessageAsync("Do you want to delete the task permanently?", "Delete task", MessageButton.OKCancel);
            if (result == MessageResult.OK)
            {
                IsLoading = true;
                string response = await _dataService.DeleteTaskAsync(_model, _listId);
                IsLoading = false;

                if (string.IsNullOrEmpty(response))
                {
                    MessengerInstance.Send<TaskDeletedMessage>(new TaskDeletedMessage(this));
                    if (_navigationService.CanGoBack())
                    {
                        _navigationService.GoBack();
                    }
                }
                else
                {
                    await _dialogService.ShowMessageAsync(response, "Error", MessageButton.OK);
                }
            }
        }

        private bool CanExecuteDeleteTaskCommand()
        {
            return true;
        }
    }
}