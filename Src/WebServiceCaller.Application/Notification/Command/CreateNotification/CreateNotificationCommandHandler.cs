using System;
using System.Threading;
using System.Threading.Tasks;
using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebServiceCaller.Application.Common.Interfaces;
using WebServiceCaller.Application.Common.Strategy;
using WebServiceCaller.Common.General;
using WebServiceCaller.Common.Result;

namespace WebServiceCaller.Application.Notification.Command.CreateNotification
{
    public class CreateNotificationCommandHandler : IRequestHandler<CreateNotificationCommand, Result>
    {
        private readonly IWebService _webService;
        private readonly IEmailService _emailService;
        private readonly IWebServiceNotificationContext _context;

        public CreateNotificationCommandHandler(IWebService webService, IEmailService emailService, IWebServiceNotificationContext context)
        {
            _webService = webService;
            _emailService = emailService;
            _context = context;
        }

        public Task<Result> Handle(CreateNotificationCommand request, CancellationToken cancellationToken)
        {
            ServiceStrategyContext context = new ServiceStrategyContext(_context, _emailService, _webService);

            //add Notification In Queue
            BackgroundJob.Schedule(() => context.ExecuteService(request, cancellationToken), TimeSpan.FromSeconds(2));

            return Task.FromResult(Result.SuccessFul(new OkObjectResult(new ApiMessage(ResponseMessage.OperationSuccessful))));
        }
    }
}