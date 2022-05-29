using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebServiceCaller.Application.Common.Interfaces;
using WebServiceCaller.Application.Common.Strategy.Interfaces;
using WebServiceCaller.Application.Notification.Command.CreateNotification;
using WebServiceCaller.Application.Notification.Command.TestNotifier;
using WebServiceCaller.Application.Notifiers.ModelDto;
using WebServiceCaller.Common.General;
using WebServiceCaller.Common.Options;
using WebServiceCaller.Domain.Enum;
using WebServiceCaller.Domain.Models;

namespace WebServiceCaller.Application.Common.Strategy.Class
{
    public class WebStrategy : IServiceStrategy
    {
        private readonly IWebServiceNotificationContext _context;
        private readonly IWebService _webService;

        public WebStrategy(IWebServiceNotificationContext context, IWebService webService)
        {
            _context = context;
            _webService = webService;
        }

        public async Task Execute(CreateNotificationCommand notification, CancellationToken cancellationToken)
        {
            var notifier = await _context.Notifiers.SingleOrDefaultAsync(x => x.Id == notification.NotifierId, cancellationToken);

            string content = string.Empty;

            if (notification.TemplateId.HasValue)
            {
                var template = await _context.Templates.SingleOrDefaultAsync(x => !x.IsDeleted && x.Id == notification.TemplateId.Value, cancellationToken);

                content = template.Content;
            }

            var setting = JsonConvert.DeserializeObject<WebServiceSetting>(notifier.Setting);

            var result = await _webService.SendMessage(notification.ToRecipient, content, notification.TagValues
                , setting);

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

            var setting = JsonConvert.DeserializeObject<WebServiceSetting>(notifier.Setting);

            var result = await _webService.SendMessage(testNotifier.ToRecipient, ResponseMessage.TestNotifierMessage, new List<Tag>()
                , setting);

        }
    }
}