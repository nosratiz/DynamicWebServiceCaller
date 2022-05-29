using MediatR;
using WebServiceCaller.Common.Result;

namespace WebServiceCaller.Application.Notifiers.Command.DeleteNotifier
{
    public class DeleteNotifierCommand : IRequest<Result>
    {
        public long Id { get; set; }
    }
}