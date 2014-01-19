using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace done.Desktop.Converters
{
    public class VisibilityConverter : IValueConverter
    {
        public virtual object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isVisible = true;

            if (value is bool)
            {
                isVisible = (bool)value;
            }
            else if (value is int || value is short || value is long)
            {
                isVisible = (0 != (int)value);
            }
            else if (value is float || value is double)
            {
                isVisible = (0.0 != (double)value);
            }
            else if (value is string)
            {
                isVisible = !string.IsNullOrWhiteSpace(value as string);
            }
            else if (value == null)
            {
                isVisible = false;
            }

            if ((string)parameter == "Invert")
                isVisible = !isVisible;

            return isVisible ? Visibility.Visible : Visibility.Collapsed;
        }

        public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Visibility visibility = (Visibility)value;
            return (visibility == Visibility.Visible);
        }
    }
}
