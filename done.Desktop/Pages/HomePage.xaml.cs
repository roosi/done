using done.Shared.ViewModels;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace done.Desktop.Pages
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        public HomePage()
        {
            InitializeComponent();

            Loaded += HomePage_Loaded;
        }

        void HomePage_Loaded(object sender, RoutedEventArgs e)
        {
            TaskListsViewModel vm = DataContext as TaskListsViewModel;
            if (vm.TaskLists.Count == 0)
            {
                vm.GetTaskListsCommand.Execute(null);
            }
        }

        private void InfoButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Pages/AboutPage.xaml", UriKind.Relative));
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox menu = sender as ListBox;

            if (menu.SelectedIndex == -1)
            {
                return;
            }

            NavigationService.Navigate(new Uri("/Pages/Page1.xaml", UriKind.Relative));

            menu.SelectedIndex = -1;
        }

        private void Tasks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox tasks = sender as ListBox;

            if (tasks.SelectedIndex == -1)
            {
                return;
            }

            NavigationService.Navigate(new Uri("/Pages/Page1.xaml", UriKind.Relative));

            tasks.SelectedIndex = -1;
        }

        private void CreateTaskListButton_Click(object sender, RoutedEventArgs e)
        {
            var flyout = ((MetroWindow)(App.Current.MainWindow)).Flyouts.Items[0] as Flyout;
            if (flyout == null)
            {
                return;
            }

            flyout.IsOpen = !flyout.IsOpen;
        }
    }
}
