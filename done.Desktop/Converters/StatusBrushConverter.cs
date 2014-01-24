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
        public Brush NewBrush = (Brush)App.Current.Resources["StatusNew"];
        public Brush NeedsActionBrush = (Brush)App.Current.Resources["StatusNeedsAction"];
        public Brush CompletedBrush = (Brush)App.Current.Resources["StatusCompleted"];
        public Brush StatusDueBrush = (Brush)App.Current.Resources["StatusDue"];
        public Brush StatusDueClosingBrush = (Brush)App.Current.Resources["StatusDueClosing"];

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            TaskViewModel task = value as TaskViewModel;
            if (task.Status.Equals(TaskViewModel.StatusNeedsAction))
            {
                if (task.DueDate.Ticks <= DateTime.Today.Ticks)
                {
                    return StatusDueBrush;
                }
                else if (task.DueDate.AddDays(-1).Ticks <= DateTime.Today.Ticks)
                {
                    return StatusDueClosingBrush;
                }
                return NeedsActionBrush;
            }
            else if (task.Status.Equals(TaskViewModel.StatusCompleted))
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
