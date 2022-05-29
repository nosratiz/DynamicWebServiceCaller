using System.Collections.Generic;
using MediatR;
using WebServiceCaller.Application.Templates.ModelDto;
using WebServiceCaller.Common.Result;

namespace WebServiceCaller.Application.Templates.Command.UpdateTemplate
{
    public class UpdateTemplateCommand : IRequest<Result<TemplateDto>>
    {
        public long Id { get; set; }
        public string Title { get; set; }

        public string Content { get; set; }

        public bool HasHtml { get; set; }

        public List<string> Tags { get; set; }
    }
}