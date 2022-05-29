using MediatR;
using WebServiceCaller.Common.Result;

namespace WebServiceCaller.Application.Templates.Command.DeleteTemplate
{
    public class DeleteTemplateCommand : IRequest<Result>
    {
        public long Id { get; set; }
    }
}