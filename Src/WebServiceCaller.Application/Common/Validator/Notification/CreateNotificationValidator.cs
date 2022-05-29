using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using WebServiceCaller.Application.Common.Interfaces;
using WebServiceCaller.Application.Notification.Command.BulkInsertNotification;
using WebServiceCaller.Application.Notification.Command.CreateNotification;
using WebServiceCaller.Common.General;

namespace WebServiceCaller.Application.Common.Validator.Notification
{
    public class CreateNotificationCommandValidator : AbstractValidator<CreateNotificationsCommand>
    {
        private readonly IWebServiceNotificationContext _context;

        public CreateNotificationCommandValidator(IWebServiceNotificationContext context)
        {
            _context = context;

            RuleForEach(x => x.Notification).NotNull();

            RuleForEach(x => x.Notification).SetValidator(new NotificationValidator(context));
        }
    }

    public class NotificationValidator : AbstractValidator<CreateNotificationCommand>
    {
        private readonly IWebServiceNotificationContext _context;

        public NotificationValidator(IWebServiceNotificationContext context)
        {
            _context = context;

            RuleFor(dto => dto.NotifierId).NotEmpty();

            RuleFor(dto => dto.ToRecipient).NotEmpty();

            RuleForEach(dto => dto.TagValues).NotNull();

            RuleFor(dto => dto).MustAsync(ValidNotifier)
                .WithMessage(ResponseMessage.NotifierNotFound)
                .MustAsync(ValidTemplate)
                .WithMessage(ResponseMessage.TemplateNotFound);
        }

        private async Task<bool> ValidNotifier(CreateNotificationCommand notification, CancellationToken cancellationToken)
        {
            if (!await _context.Notifiers.AnyAsync(x => !x.IsDeleted && x.Id == notification.NotifierId, cancellationToken))
                return false;

            return true;
        }

        private async Task<bool> ValidTemplate(CreateNotificationCommand notification, CancellationToken cancellationToken)
        {
            if (notification.TemplateId.HasValue)
                if (!await _context.Templates.AnyAsync(x => !x.IsDeleted && x.Id == notification.TemplateId, cancellationToken))
                    return false;

            return true;
        }
    }
}