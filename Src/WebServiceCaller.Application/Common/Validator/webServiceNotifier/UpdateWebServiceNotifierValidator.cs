using FluentValidation;
using WebServiceCaller.Application.Notifiers.Command.UpdateWebService;
using WebServiceCaller.Common.General;
using WebServiceCaller.Domain.Enum;

namespace WebServiceCaller.Application.Common.Validator.webServiceNotifier
{
    public class UpdateWebServiceNotifierValidator : AbstractValidator<UpdateWebServiceCommand>
    {
        public UpdateWebServiceNotifierValidator()
        {
            RuleFor(dto => dto.Id).NotEmpty().WithMessage(ResponseMessage.IdIsRequired);

            RuleFor(dto => dto.Name).NotEmpty().WithMessage(ResponseMessage.NameIsRequired);

            RuleFor(dto => dto.ContentType).NotEmpty().WithMessage(ResponseMessage.ContentTypeIsRequired);

            RuleFor(dto => dto.Method).NotEmpty().WithMessage(ResponseMessage.MethodIsRequired);

            RuleFor(dto => dto.Url).NotEmpty().WithMessage(ResponseMessage.UrlIsRequired);

            RuleFor(dto => dto).Must(ValidBody).WithMessage(ResponseMessage.BodyMustContainContentandTo);

        }

        private bool ValidBody(UpdateWebServiceCommand updateWebServiceCommand)
        {

            if (updateWebServiceCommand.Method == HttpMethod.Post)
                if (updateWebServiceCommand.Body.Contains("%content%") && updateWebServiceCommand.Body.Contains("%to%"))
                    return true;

            return false;
        }


    }
}