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
using Windows.Phone.Speech.Recognition;

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
            string value = ((TextBox)sender).Text;
            if (value.Equals(_vm.Title) == false)
            {
                _vm.Title = value;
            }
        }

        private void Notes_TextChanged(object sender, TextChangedEventArgs e)
        {
            string value = ((TextBox)sender).Text;
            if (value.Equals(_vm.Notes) == false)
            {
                _vm.Notes = value;
            }
        }

        private async void VoiceRecognitionButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SpeechRecognizerUI sr = new SpeechRecognizerUI();

                SpeechRecognitionUIResult result = await sr.RecognizeWithUIAsync();

                if (result.ResultStatus == SpeechRecognitionUIStatus.Succeeded)
                {
                    if (string.IsNullOrEmpty(_vm.Notes) == false)
                    {
                        _vm.Notes += " ";
                    }
                    _vm.Notes += result.RecognitionResult.Text;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Voice recognition is not enabled. Check your settings.");
            }
        }
    }
}