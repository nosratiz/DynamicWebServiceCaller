using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebServiceCaller.Application.Common.Interfaces;
using WebServiceCaller.Application.Common.Strategy.Interfaces;
using WebServiceCaller.Application.Notification.Command.CreateNotification;
using WebServiceCaller.Application.Notification.Command.TestNotifier;
using WebServiceCaller.Common.General;
using WebServiceCaller.Common.Helper;
using WebServiceCaller.Common.Options;
using WebServiceCaller.Domain.Enum;
using WebServiceCaller.Domain.Models;

namespace WebServiceCaller.Application.Common.Strategy.Class
{
    public class EmailStrategy : IServiceStrategy
    {
        private readonly IWebServiceNotificationContext _context;
        private readonly IEmailService _emailService;

        public EmailStrategy(IWebServiceNotificationContext context, IEmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        public async Task Execute(CreateNotificationCommand notification, CancellationToken cancellationToken)
        {
            var notifier = await _context.Notifiers.SingleOrDefaultAsync(x => x.Id == notification.NotifierId, cancellationToken);

            var template = await _context.Templates.SingleOrDefaultAsync(x => x.Id == notification.TemplateId, cancellationToken);

            var content = Utils.InterpolateTags(template.Content, notification.TagValues);

            var result = await _emailService.SendMessage(notification.ToRecipient, template.Title, content,
                template.HasHtml, JsonConvert.DeserializeObject<EmailSetting>(notifier.Setting));

            #region Add Notifier Log

            await _context.NotifierLogs.AddAsync(new NotifierLog
            {
                CreateDate = DateTime.Now,
                Description = result.Item2,
                logStatus = result.Item1 ? LogStatus.Success : LogStatus.Failed,
                NotifierId = notifier.Id
            }, cancellationToken);

            await _context.SaveAsync(cancellationToken);

            #endregion Add Notifier Log
        }


        public async Task TestExecute(CreateTestNotifierCommand testNotifier, CancellationToken cancellationToken)
        {
            var notifier = await _context.Notifiers.SingleOrDefaultAsync(x => x.Id == testNotifier.NotifierId, cancellationToken);


            await _emailService.SendMessage(testNotifier.ToRecipient, ResponseMessage.TitleTestMessage, ResponseMessage.TestNotifierMessage,
                false, JsonConvert.DeserializeObject<EmailSetting>(notifier.Setting));

        }
    }
}