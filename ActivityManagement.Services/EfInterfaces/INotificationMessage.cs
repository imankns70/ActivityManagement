using System.Collections.Generic;
using ActivityManagement.ViewModels.Base;

namespace ActivityManagement.Services.EfInterfaces
{
    public interface INotificationMessage
    {

        List<NotificationModel> MyNot { get; }
      
    }
}