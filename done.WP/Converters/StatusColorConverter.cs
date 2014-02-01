using done.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace done.WP.Converters
{
    public class StatusColorConverter : IValueConverter
    {
        public Color NewColor = (Color)App.Current.Resources["StatusNewColor"];
        public Color NeedsActionColor = (Color)App.Current.Resources["StatusNeedsActionColor"];
        public Color CompletedColor = (Color)App.Current.Resources["StatusCompletedColor"];
        public Color StatusDueColor = (Color)App.Current.Resources["StatusDueColor"];
        public Color StatusDueClosingColor = (Color)App.Current.Resources["StatusDueClosingColor"];

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            TaskViewModel.StatusInfo info = value as TaskViewModel.StatusInfo;
            if (info.Status.Equals(TaskViewModel.StatusNeedsAction))
            {
                if (info.DueDate.Ticks <= DateTime.Today.Ticks)
                {
                    return StatusDueColor;
                }
                else if (info.DueDate.AddDays(-1).Ticks <= DateTime.Today.Ticks)
                {
                    return StatusDueClosingColor;
                }
                return NeedsActionColor;
            }
            else if (info.Status.Equals(TaskViewModel.StatusCompleted))
            {
                return CompletedColor;
            }

            return NewColor;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
