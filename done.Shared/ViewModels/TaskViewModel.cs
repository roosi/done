using done.Shared.Services;
using Google.Apis.Tasks.v1.Data;
using System;

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
        private Task _model;

        /// <summary>
        /// Initializes a new instance of the TaskViewModel class.
        /// </summary>
        public TaskViewModel(Task model, IDataService dataService, INavigationService navigationService)
            : base(dataService, navigationService)
        {
            _model = model;
            _title = model.Title;
            _status = model.Status ?? _status;
            _dueDate = model.Due != null ? model.Due.Value : DateTime.Today;
            _notes = model.Notes;
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
                Set(TitlePropertyName, ref _title, value);
            }
        }

        /// <summary>
        /// The <see cref="Status" /> property's name.
        /// </summary>
        public const string StatusPropertyName = "Status";

        private string _status = "new";

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
                Set(StatusPropertyName, ref _status, value);
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
                Set(DueDatePropertyName, ref _dueDate, value);
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
                Set(NotesPropertyName, ref _notes, value);
            }
        }
    }
}