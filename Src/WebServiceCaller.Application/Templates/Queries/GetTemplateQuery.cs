using MediatR;
using WebServiceCaller.Application.Templates.ModelDto;
using WebServiceCaller.Common.Result;

namespace WebServiceCaller.Application.Templates.Queries
{
    public class GetTemplateQuery : IRequest<Result<TemplateDto>>
    {
        public long Id { get; set; }
    }
}