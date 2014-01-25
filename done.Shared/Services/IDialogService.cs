using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace done.Shared.Services
{
    public enum MessageResult
    {
        Cancel,
        No,
        None,
        OK,
        Yes
    }

    public enum MessageButton
    {
        OK,
        OKCancel,
        YesNo,
        YesNoCancel
    }

    public interface IDialogService
    {
        MessageResult ShowMessage(string message, string caption, MessageButton button);
        Task<MessageResult> ShowMessageAsync(string message, string caption, MessageButton button);
    }
}
