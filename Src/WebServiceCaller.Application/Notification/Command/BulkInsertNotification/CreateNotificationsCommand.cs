using System.Collections.Generic;
using MediatR;
using WebServiceCaller.Application.Notification.Command.CreateNotification;
using WebServiceCaller.Common.Result;

namespace WebServiceCaller.Application.Notification.Command.BulkInsertNotification
{
    public class CreateNotificationsCommand : IRequest<Result>
    {
        public List<CreateNotificationCommand> Notification { get; set; }
    }
}