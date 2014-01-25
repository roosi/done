using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace done.Shared.Services
{
    public interface INavigationService
    {
        void Navigate(Type viewModelType);
        void GoBack();
        bool CanGoBack();
    }
}
