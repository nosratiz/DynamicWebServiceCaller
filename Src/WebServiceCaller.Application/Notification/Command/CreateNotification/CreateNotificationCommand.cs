using System.Collections.Generic;
using MediatR;
using WebServiceCaller.Common.Options;
using WebServiceCaller.Common.Result;

namespace WebServiceCaller.Application.Notification.Command.CreateNotification
{
    public class CreateNotificationCommand : IRequest<Result>
    {
        public string ToRecipient { get; set; }

        public long NotifierId { get; set; }

        public long? TemplateId { get; set; }

        public List<Tag> TagValues { get; set; }
    }
}