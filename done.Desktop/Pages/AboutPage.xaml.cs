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
    /// Interaction logic for AboutPage.xaml
    /// </summary>
    public partial class AboutPage : Page
    {
        public AboutPage()
        {
            InitializeComponent();

            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();

            Version version = assembly.GetName().Version;

            ApplicationName.Text = assembly.GetName().Name;
            ApplicationVersion.Text = "v. " + assembly.GetName().Version.ToString();

            Loaded += AboutPage_Loaded;
        }

        void AboutPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (NavigationService.CanGoBack)
            {
                BackButton.Visibility = Visibility.Visible;
            }

        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
