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

namespace WebServiceCaller.Application.Notification.Command.BulkInsertNotification
{
    public class CreateNotificationsCommandHandler : IRequestHandler<CreateNotificationsCommand, Result>
    {
        private readonly IWebServiceNotificationContext _context;
        private readonly IEmailService _emailService;
        private readonly IWebService _webService;

        public CreateNotificationsCommandHandler(IWebServiceNotificationContext context, IEmailService emailService, IWebService webService)
        {
            _context = context;
            _emailService = emailService;
            _webService = webService;
        }

        public Task<Result> Handle(CreateNotificationsCommand request, CancellationToken cancellationToken)
        {
            ServiceStrategyContext context = new ServiceStrategyContext(_context, _emailService, _webService);

            request.Notification.ForEach(notification =>
           {
               //add Notification In Queue
               BackgroundJob.Schedule(() => context.ExecuteService(notification, cancellationToken), TimeSpan.FromSeconds(2));
           });

            return Task.FromResult(Result.SuccessFul(new OkObjectResult(new ApiMessage(ResponseMessage.OperationSuccessful))));
        }
    }
}