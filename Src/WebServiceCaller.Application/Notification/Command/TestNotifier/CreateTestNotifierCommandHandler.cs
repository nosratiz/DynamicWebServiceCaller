using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebServiceCaller.Application.Common.Interfaces;
using WebServiceCaller.Application.Common.Strategy;
using WebServiceCaller.Common.General;
using WebServiceCaller.Common.Result;

namespace WebServiceCaller.Application.Notification.Command.TestNotifier
{
    public class CreateTestNotifierCommandHandler : IRequestHandler<CreateTestNotifierCommand, Result>
    {
        private readonly IWebServiceNotificationContext _context;
        private readonly IWebService _webService;
        private readonly IEmailService _emailService;

        public CreateTestNotifierCommandHandler(IWebServiceNotificationContext context, IWebService webService, IEmailService emailService)
        {
            _context = context;
            _webService = webService;
            _emailService = emailService;
        }

        public async Task<Result> Handle(CreateTestNotifierCommand request, CancellationToken cancellationToken)
        {
            var notifier =
                await _context.Notifiers.SingleOrDefaultAsync(x => !x.IsDeleted && x.Id == request.NotifierId, cancellationToken);

            if (notifier is null)
                return Result.Failed(new BadRequestObjectResult(new ApiMessage(ResponseMessage.NotifierNotFound)));

            ServiceStrategyContext context = new ServiceStrategyContext(_context, _emailService, _webService);

            await context.TestExecuteService(request, cancellationToken);

            return Result.SuccessFul(new OkObjectResult(new ApiMessage(ResponseMessage.OperationSuccessful)));
        }
    }
}