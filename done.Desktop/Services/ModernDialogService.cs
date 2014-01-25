using done.Shared.Services;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace done.Desktop.Services
{
    public class ModernDialogService : IDialogService
    {
        public async Task<MessageResult> ShowMessageAsync(string message, string caption, MessageButton button)
        {
            var settings = new MetroDialogSettings()
            {
                AffirmativeButtonText = null,
                NegativeButtonText = null,
                FirstAuxiliaryButtonText = null,
                SecondAuxiliaryButtonText = null,
                UseAnimations = true
            };

            MessageDialogStyle style = MessageDialogStyle.Affirmative;

            switch (button)
            {
                case MessageButton.OK:
                    settings.AffirmativeButtonText = "0K";
                    break;
                case MessageButton.OKCancel:
                    settings.AffirmativeButtonText = "OK";
                    settings.NegativeButtonText = "Cancel";
                    style = MessageDialogStyle.AffirmativeAndNegative;
                    break;
                case MessageButton.YesNo:
                    settings.AffirmativeButtonText = "Yes";
                    settings.NegativeButtonText = "No";
                    style = MessageDialogStyle.AffirmativeAndNegative;
                    break;
                case MessageButton.YesNoCancel:
                    settings.AffirmativeButtonText = "Yes";
                    settings.NegativeButtonText = "No";
                    settings.FirstAuxiliaryButtonText = "Cancel";
                    style = MessageDialogStyle.AffirmativeAndNegativeAndSingleAuxiliary;
                    break;
                default:
                    break;
            }

            MessageDialogResult resultImp = await ((MetroWindow)(App.Current.MainWindow)).ShowMessageAsync(caption, message, 
                style, settings);

            MessageResult result = MessageResult.None;

            switch (resultImp)
            {
                case MessageDialogResult.Affirmative:
                    //TODO Yes
                    result = MessageResult.OK;
                    break;
                case MessageDialogResult.FirstAuxiliary:
                    //TODO No
                    result = MessageResult.Cancel;
                    break;
                case MessageDialogResult.Negative:
                    result = MessageResult.Cancel;
                    break;
                case MessageDialogResult.SecondAuxiliary:
                    break;
                default:
                    break;
            }

            return result;
        }

        MessageResult IDialogService.ShowMessage(string message, string caption, MessageButton button)
        {
            throw new NotImplementedException();
        }
    }
}
