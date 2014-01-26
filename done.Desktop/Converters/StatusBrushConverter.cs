using done.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace done.Desktop.Converters
{
    public class StatusBrushConverter : IValueConverter
    {
        public SolidColorBrush NewBrush = (SolidColorBrush)App.Current.Resources["StatusNew"];
        public SolidColorBrush NeedsActionBrush = (SolidColorBrush)App.Current.Resources["StatusNeedsAction"];
        public SolidColorBrush CompletedBrush = (SolidColorBrush)App.Current.Resources["StatusCompleted"];
        public SolidColorBrush StatusDueBrush = (SolidColorBrush)App.Current.Resources["StatusDue"];
        public SolidColorBrush StatusDueClosingBrush = (SolidColorBrush)App.Current.Resources["StatusDueClosing"];

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            TaskViewModel.StatusInfo info = value as TaskViewModel.StatusInfo;
            if (info.Status.Equals(TaskViewModel.StatusNeedsAction))
            {
                if (info.DueDate.Ticks <= DateTime.Today.Ticks)
                {
                    return StatusDueBrush;
                }
                else if (info.DueDate.AddDays(-1).Ticks <= DateTime.Today.Ticks)
                {
                    return StatusDueClosingBrush;
                }
                return NeedsActionBrush;
            }
            else if (info.Status.Equals(TaskViewModel.StatusCompleted))
            {
                return CompletedBrush;
            }

            return NewBrush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
