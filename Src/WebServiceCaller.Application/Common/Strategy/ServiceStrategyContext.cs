using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebServiceCaller.Application.Common.Interfaces;
using WebServiceCaller.Application.Common.Strategy.Class;
using WebServiceCaller.Application.Common.Strategy.Interfaces;
using WebServiceCaller.Application.Notification.Command.CreateNotification;
using WebServiceCaller.Application.Notification.Command.TestNotifier;
using WebServiceCaller.Domain.Enum;

namespace WebServiceCaller.Application.Common.Strategy
{
    public class ServiceStrategyContext
    {
        private IServiceStrategy _serviceStrategy;
        private readonly IWebServiceNotificationContext _context;
        private readonly IEmailService _emailService;
        private readonly IWebService _webService;

        public ServiceStrategyContext(IWebServiceNotificationContext context, IEmailService emailService, IWebService webService)
        {
            _context = context;
            _emailService = emailService;
            _webService = webService;
        }

        public void SetStrategy(IServiceStrategy serviceStrategy)
        {
            _serviceStrategy = serviceStrategy;
        }

        public async Task ExecuteService(CreateNotificationCommand notification, CancellationToken cancellationToken)
        {
            var notifier =
                await _context.Notifiers.SingleOrDefaultAsync(x => !x.IsDeleted && x.Id == notification.NotifierId, cancellationToken);

            if (notifier.ServiceType == ServiceType.Smtp)
                SetStrategy(new EmailStrategy(_context, _emailService));

            else
                SetStrategy(new WebStrategy(_context, _webService));

            await _serviceStrategy.Execute(notification, cancellationToken);
        }


        public async Task TestExecuteService(CreateTestNotifierCommand notification, CancellationToken cancellationToken)
        {
            var notifier =
                await _context.Notifiers.SingleOrDefaultAsync(x => !x.IsDeleted && x.Id == notification.NotifierId, cancellationToken);

            if (notifier.ServiceType == ServiceType.Smtp)
                SetStrategy(new EmailStrategy(_context, _emailService));

            else
                SetStrategy(new WebStrategy(_context, _webService));

            await _serviceStrategy.TestExecute(notification, cancellationToken);
        }

    }
}