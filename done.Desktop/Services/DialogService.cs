using done.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace done.Desktop.Services
{
    public class DialogService : IDialogService
    {
        public MessageResult ShowMessage(string message, string caption, MessageButton button)
        {
            MessageBoxButton impButton = MessageBoxButton.OK;

            switch (button)
            {
                case MessageButton.OK:
                    impButton = MessageBoxButton.OK;
                    break;
                case MessageButton.OKCancel:
                    impButton = MessageBoxButton.OKCancel;
                    break;
                case MessageButton.YesNo:
                    impButton = MessageBoxButton.YesNo;
                    break;
                case MessageButton.YesNoCancel:
                    impButton = MessageBoxButton.YesNoCancel;
                    break;
                default:
                    break;
            }

            MessageBoxResult impResult = MessageBox.Show(message, caption, impButton);

            MessageResult result = MessageResult.OK;

            switch (impResult)
            {
                case MessageBoxResult.Cancel:
                    result = MessageResult.Cancel;
                    break;
                case MessageBoxResult.No:
                    result = MessageResult.No;
                    break;
                case MessageBoxResult.None:
                    result = MessageResult.None;
                    break;
                case MessageBoxResult.OK:
                    result = MessageResult.OK;
                    break;
                case MessageBoxResult.Yes:
                    result = MessageResult.Yes;
                    break;
                default:
                    break;
            }

            return result;
        }


        public Task<MessageResult> ShowMessageAsync(string message, string caption, MessageButton button)
        {
            throw new NotImplementedException();
        }
    }
}
