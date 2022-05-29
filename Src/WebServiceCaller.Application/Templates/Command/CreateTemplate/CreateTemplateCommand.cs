using System.Collections.Generic;
using MediatR;
using WebServiceCaller.Application.Templates.ModelDto;
using WebServiceCaller.Common.Result;

namespace WebServiceCaller.Application.Templates.Command.CreateTemplate
{
    public class CreateTemplateCommand : IRequest<Result<TemplateDto>>
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public bool HasHtml { get; set; }

        public List<string> Tags { get; set; }
    }
}