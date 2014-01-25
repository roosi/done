using done.Shared.ViewModels;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace done.Shared.Messages
{
    public class TaskDeletedMessage : GenericMessage<TaskViewModel>
    {
        public TaskDeletedMessage(TaskViewModel content)
            : base(content)
        {
        }
    }
}
