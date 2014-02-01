using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using done.WP.Resources;
using done.Shared.ViewModels;
using done.Shared.Services;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Tasks.v1;
using System.Threading;

namespace done.WP
{
    public partial class MainPage : PhoneApplicationPage
    {
        private const String _clientId = "552948890600.apps.googleusercontent.com";
        private const String _clientSecret = "H_AA4PQQvKtmcHYubTd5SEHi";

        private TaskListsViewModel _vm = null;

        // Constructor
        public MainPage()
        {
            InitializeComponent();

            _vm = DataContext as TaskListsViewModel;
            _vm.EditMode = true;

            ((ApplicationBarMenuItem)ApplicationBar.MenuItems[0]).Click += AboutMenu_Click;
        }

        void AboutMenu_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/View/AboutPage.xaml", UriKind.Relative));
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.NavigationMode == NavigationMode.New)
            {
                ViewModelLocator locator = App.Current.Resources["Locator"] as ViewModelLocator;
                IDataService dataService = locator.Data;

                UserCredential credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                    new ClientSecrets
                    {
                        ClientId = _clientId,
                        ClientSecret = _clientSecret
                    },
                    new[] { TasksService.Scope.Tasks },
                    "user",
                    CancellationToken.None);

                dataService.SetUserCredential(credential);

                _vm.GetTaskListsCommand.Execute(null);
            }
            else if (e.NavigationMode == NavigationMode.Back)
            {
                if (_vm.SelectedTaskList != null)
                {
                    _vm.SelectedTaskList.SelectedTask = null;
                }
            }
        }
    }
}