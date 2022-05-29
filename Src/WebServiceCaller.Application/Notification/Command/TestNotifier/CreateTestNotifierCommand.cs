using MediatR;
using WebServiceCaller.Common.Result;

namespace WebServiceCaller.Application.Notification.Command.TestNotifier
{
    public class CreateTestNotifierCommand : IRequest<Result>
    {
        public long NotifierId { get; set; }

        public string ToRecipient { get; set; }
    }
}