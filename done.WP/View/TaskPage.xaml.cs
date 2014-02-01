using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using done.Shared.ViewModels;

namespace done.WP.View
{
    public partial class TaskPage : PhoneApplicationPage
    {
        private TaskViewModel _vm = null;

        public TaskPage()
        {
            InitializeComponent();

            _vm = DataContext as TaskViewModel;
        }

        private void Title_TextChanged(object sender, TextChangedEventArgs e)
        {
            _vm.Title = ((TextBox)sender).Text;
        }

        private void Notes_TextChanged(object sender, TextChangedEventArgs e)
        {
            _vm.Notes = ((TextBox)sender).Text;
        }
    }
}