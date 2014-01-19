using done.Shared.Services;
using done.Shared.ViewModels;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Tasks.v1;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace done.Desktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private const String _clientId = "552948890600.apps.googleusercontent.com";
        private const String _clientSecret = "H_AA4PQQvKtmcHYubTd5SEHi";

        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        async void MainWindow_Loaded(object sender, RoutedEventArgs e)
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
        }
    }
}
