using FluentValidation;
using WebServiceCaller.Application.Notifiers.Command.CreateWebService;
using WebServiceCaller.Common.General;
using WebServiceCaller.Domain.Enum;

namespace WebServiceCaller.Application.Common.Validator.webServiceNotifier
{
    public class CreateWebServiceNotifierValidator : AbstractValidator<CreateWebServiceCommand>
    {
        public CreateWebServiceNotifierValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(dto => dto.Name).NotEmpty().WithMessage(ResponseMessage.NameIsRequired).NotNull().WithMessage(ResponseMessage.NameIsRequired);

            RuleFor(dto => dto.ContentType).NotEmpty().WithMessage(ResponseMessage.ContentTypeIsRequired);

            RuleFor(dto => dto.Method).NotEmpty().WithMessage(ResponseMessage.MethodIsRequired);

            RuleFor(dto => dto.Url).NotEmpty().WithMessage(ResponseMessage.UrlIsRequired);

            RuleFor(dto => dto).Must(ValidBody).WithMessage(ResponseMessage.BodyMustContainContentandTo);
        }


        private bool ValidBody(CreateWebServiceCommand createWebServiceCommand)
        {

            if (createWebServiceCommand.Method == HttpMethod.Post && createWebServiceCommand.ContentType!=ContentType.Xml)
                if (createWebServiceCommand.Body.Contains("%content%") && createWebServiceCommand.Body.Contains("%to%"))
                    return false;

            return true;
        }
    }
}