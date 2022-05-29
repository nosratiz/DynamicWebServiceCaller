using System.Linq;
using FluentValidation;
using WebServiceCaller.Application.Templates.Command.UpdateTemplate;
using WebServiceCaller.Common.General;

namespace WebServiceCaller.Application.Common.Validator.Template
{
    public class UpdateTemplateCommandValidator : AbstractValidator<UpdateTemplateCommand>
    {
        public UpdateTemplateCommandValidator()
        {
            RuleFor(dto => dto.Content).NotEmpty().WithMessage(ResponseMessage.ContentIsRequired);

            RuleFor(dto => dto.Title).NotEmpty().WithMessage(ResponseMessage.TitleIsRequired);

            RuleFor(dto => dto.Tags).NotEmpty().WithMessage(ResponseMessage.TagsIsRequired);

            RuleFor(dto => dto).Must(ValidTag).WithMessage(ResponseMessage.TagNotFind)
                .Must(DuplicateTagChecking).WithMessage(ResponseMessage.DuplicateTag);
        }

        private bool ValidTag(UpdateTemplateCommand updateTemplateCommand)
        {
            foreach (var item in updateTemplateCommand.Tags)
                if (!updateTemplateCommand.Content.Contains($"%{item}%"))
                    return false;

            return true;
        }

        private bool DuplicateTagChecking(UpdateTemplateCommand updateTemplateCommand)
        {
            if (updateTemplateCommand.Tags.GroupBy(x => x).Any(x => x.Count() > 1))
                return false;

            return true;
        }
    }
}