using done.Desktop.Pages;
using done.Shared.Services;
using done.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace done.Desktop.Services
{
    public class NavigationService : INavigationService
    {
        /// <summary> 
        /// The view model routing. 
        /// </summary> 
        private static readonly IDictionary<Type, Type> ViewModelRouting = new Dictionary<Type, Type>() 
        { 
            { 
                typeof(TaskViewModel), typeof(TaskPage) 
            } 
        };

        public void Navigate(Type viewModelType)
        {
            Type pageType = ViewModelRouting[viewModelType];

            var page = string.Format("/Pages/{0}.xaml", pageType.Name);
            ((Frame)App.Current.MainWindow.Content).Navigate(new Uri(page, UriKind.Relative));
        }

        public void GoBack()
        {
            ((Frame)App.Current.MainWindow.Content).GoBack();
        }
    }
}
