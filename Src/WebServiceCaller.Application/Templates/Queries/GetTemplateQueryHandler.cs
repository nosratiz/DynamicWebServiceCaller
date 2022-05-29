using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebServiceCaller.Application.Common.Interfaces;
using WebServiceCaller.Application.Templates.ModelDto;
using WebServiceCaller.Common.General;
using WebServiceCaller.Common.Result;

namespace WebServiceCaller.Application.Templates.Queries
{
    public class GetTemplateQueryHandler : IRequestHandler<GetTemplateQuery, Result<TemplateDto>>
    {
        private readonly IWebServiceNotificationContext _context;
        private readonly IMapper _mapper;

        public GetTemplateQueryHandler(IWebServiceNotificationContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<TemplateDto>> Handle(GetTemplateQuery request, CancellationToken cancellationToken)
        {
            var template =
                await _context.Templates.SingleOrDefaultAsync(x => !x.IsDeleted && x.Id == request.Id,
                    cancellationToken);

            if (template is null)
                return Result<TemplateDto>.Failed(new NotFoundObjectResult(new ApiMessage(ResponseMessage.TemplateNotFound)));

            return Result<TemplateDto>.SuccessFul(_mapper.Map<TemplateDto>(template));
        }
    }
}