using done.Shared.Services;
using done.Shared.ViewModels;
using done.WP.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace done.WP.Services
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

            var page = string.Format("/View/{0}.xaml", pageType.Name);
            App.RootFrame.Navigate(new Uri(page, UriKind.Relative));
        }

        public void GoBack()
        {
            App.RootFrame.GoBack();
        }

        public bool CanGoBack()
        {
            return App.RootFrame.CanGoBack;
        }
    }
}
