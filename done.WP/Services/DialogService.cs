using done.Shared.Services;
using Microsoft.Phone.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace done.WP.Services
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
                    impButton = MessageBoxButton.OKCancel;
                    break;
                case MessageButton.YesNoCancel:
                    throw new NotImplementedException();
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
            var tcs = new TaskCompletionSource<MessageResult>();

            CustomMessageBox messageBox = new CustomMessageBox()
            {
                Caption = caption,
                Message = message
            };

            switch (button)
            {
                case MessageButton.OK:
                    messageBox.LeftButtonContent = "OK";
                    messageBox.RightButtonContent = "Cancel";
                    break;
                case MessageButton.OKCancel:
                    messageBox.LeftButtonContent = "OK";
                    messageBox.RightButtonContent = "Cancel";                    
                    break;
                case MessageButton.YesNo:
                    messageBox.LeftButtonContent = "Yes";
                    messageBox.RightButtonContent = "No";
                    break;
                case MessageButton.YesNoCancel:
                    throw new NotImplementedException();
                default:
                    break;
            }

            MessageResult result = MessageResult.OK;

            messageBox.Dismissed += (s1, e1) =>
            {
                switch (e1.Result)
                {
                    case CustomMessageBoxResult.LeftButton:
                        break;
                    case CustomMessageBoxResult.RightButton:
                        result = MessageResult.Cancel;
                        break;
                    case CustomMessageBoxResult.None:
                        // Do something.
                        break;
                    default:
                        break;
                }

                tcs.SetResult(result);
            };

            messageBox.Show();
            return tcs.Task;
        }
    }
}
