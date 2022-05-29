using FluentValidation;
using WebServiceCaller.Application.Notifiers.Command.CreateSmtpNotifier;
using WebServiceCaller.Common.General;

namespace WebServiceCaller.Application.Common.Validator.SmtpNotifier
{
    public class CreateSmtpNotifierValidator : AbstractValidator<CreateSmtpNotifierCommand>
    {
        public CreateSmtpNotifierValidator()
        {
            RuleFor(dto => dto.Name).NotEmpty().WithMessage(ResponseMessage.NameIsRequired);

            RuleFor(dto => dto.UserName).NotEmpty().WithMessage(ResponseMessage.UserNameIsRequired)
                .EmailAddress().WithMessage(ResponseMessage.EmailIsNotWellFormed);

            RuleFor(dto => dto.Password).NotEmpty().WithMessage(ResponseMessage.PasswordIsRequired);

            RuleFor(dto => dto.Host).NotEmpty().WithMessage(ResponseMessage.HostIsRequired);

            RuleFor(dto => dto.Port).NotEmpty().WithMessage(ResponseMessage.PortIsRequired)
                .GreaterThan(0).WithMessage(ResponseMessage.PortIsNotAllowed);

            RuleFor(dto => dto.From).NotEmpty().WithMessage(ResponseMessage.FromIsRequired);
        }
    }
}