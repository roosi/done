using done.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace done.WP.Services
{
    public class DialogService : IDialogService
    {
        public MessageResult ShowMessage(string message, string caption, MessageButton button)
        {
            throw new NotImplementedException();
        }

        public Task<MessageResult> ShowMessageAsync(string message, string caption, MessageButton button)
        {
            throw new NotImplementedException();
        }
    }
}
