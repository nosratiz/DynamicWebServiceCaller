using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebServiceCaller.Application.Common.Interfaces;
using WebServiceCaller.Common.General;
using WebServiceCaller.Common.Result;

namespace WebServiceCaller.Application.Notifiers.Command.DeleteNotifier
{
    public class DeleteNotifierCommandHandler : IRequestHandler<DeleteNotifierCommand, Result>
    {
        private readonly IWebServiceNotificationContext _context;

        public DeleteNotifierCommandHandler(IWebServiceNotificationContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(DeleteNotifierCommand request, CancellationToken cancellationToken)
        {
            var notifier =
                await _context.Notifiers.SingleOrDefaultAsync(x => !x.IsDeleted && x.Id == request.Id,
                    cancellationToken);

            if (notifier is null)
                return Result.Failed(new NotFoundObjectResult(new ApiMessage(ResponseMessage.NotifierNotFound)));

            notifier.IsDeleted = true;

            await _context.SaveAsync(cancellationToken);

            return Result.SuccessFul(new OkObjectResult(new ApiMessage(ResponseMessage.OperationSuccessful)));
        }
    }
}