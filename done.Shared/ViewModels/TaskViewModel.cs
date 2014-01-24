﻿using done.Shared.Services;
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
        public TaskViewModel(Task model, string listId, IDataService dataService, INavigationService navigationService)
            : base(dataService, navigationService)
        {
            _model = model;
            _listId = listId;
            update();
        }

        private void update()
        {
            Title = _model.Title;
            Status = _model.Status;
            DueDate = _model.Due != null ? _model.Due.Value : DateTime.Today;
            Notes = _model.Notes;
        }

        private void updateModel()
        {
            _model.Title = _title;
            _model.Status = _status;
            _model.Due = _dueDate != null ? _dueDate : DateTime.Today;
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
                    IsCompleted = _status.Equals(StatusCompleted);
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
                IsEdited = Set(DueDatePropertyName, ref _dueDate, value);
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
            _status = StatusCompleted;
            ExecuteUpdateTaskCommand();
        }

        private bool CanExecuteCompleteTaskCommamd()
        {
            return IsCompleted == false;
        }
    }
}