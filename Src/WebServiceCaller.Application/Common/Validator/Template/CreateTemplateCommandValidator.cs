using System.Linq;
using FluentValidation;
using WebServiceCaller.Application.Templates.Command.CreateTemplate;
using WebServiceCaller.Common.General;

namespace WebServiceCaller.Application.Common.Validator.Template
{
    public class CreateTemplateCommandValidator : AbstractValidator<CreateTemplateCommand>
    {
        public CreateTemplateCommandValidator()
        {
            RuleFor(dto => dto.Content).NotEmpty().WithMessage(ResponseMessage.ContentIsRequired);

            RuleFor(dto => dto.Title).NotEmpty().WithMessage(ResponseMessage.TitleIsRequired);

            RuleFor(dto => dto.Tags).NotEmpty().WithMessage(ResponseMessage.TagsIsRequired);

            RuleFor(dto => dto).Must(ValidTag).WithMessage(ResponseMessage.TagNotFind)
                .Must(DuplicateTagChecking).WithMessage(ResponseMessage.DuplicateTag);
        }

        private bool ValidTag(CreateTemplateCommand createTemplateCommand)
        {
            foreach (var item in createTemplateCommand.Tags)
                if (!createTemplateCommand.Content.Contains($"%{item}%"))
                    return false;

            return true;
        }

        private bool DuplicateTagChecking(CreateTemplateCommand createTemplateCommand)
        {
            if (createTemplateCommand.Tags.GroupBy(x => x).Any(x => x.Count() > 1))
                return false;

            return true;
        }
    }
}