using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebServiceCaller.Application.Common.Interfaces;
using WebServiceCaller.Common.General;
using WebServiceCaller.Common.Result;

namespace WebServiceCaller.Application.Templates.Command.DeleteTemplate
{
    public class DeleteTemplateCommandHandler : IRequestHandler<DeleteTemplateCommand, Result>
    {
        private readonly IWebServiceNotificationContext _context;

        public DeleteTemplateCommandHandler(IWebServiceNotificationContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(DeleteTemplateCommand request, CancellationToken cancellationToken)
        {
            var template = await _context.Templates.SingleOrDefaultAsync(x => !x.IsDeleted && x.Id == request.Id, cancellationToken);

            if (template is null)
                return Result.Failed(new NotFoundObjectResult(new ApiMessage(ResponseMessage.TemplateNotFound)));

            template.IsDeleted = true;

            await _context.SaveAsync(cancellationToken);

            return Result.SuccessFul(new ObjectResult(new ApiMessage(ResponseMessage.OperationSuccessful)));
        }
    }
}