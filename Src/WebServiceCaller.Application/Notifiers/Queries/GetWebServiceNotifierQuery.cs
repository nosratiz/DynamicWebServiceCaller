using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebServiceCaller.Application.Common.Interfaces;
using WebServiceCaller.Application.Notifiers.ModelDto;
using WebServiceCaller.Common.General;
using WebServiceCaller.Common.Result;
using WebServiceCaller.Domain.Enum;

namespace WebServiceCaller.Application.Notifiers.Queries
{
    public class GetWebServiceNotifierQuery : IRequest<Result<WebServiceNotifierDto>>
    {
        public long Id { get; set; }
    }

    public class GetWebServiceNotifierQueryHandler : IRequestHandler<GetWebServiceNotifierQuery, Result<WebServiceNotifierDto>>
    {
        private readonly IWebServiceNotificationContext _context;
        private readonly IMapper _mapper;

        public GetWebServiceNotifierQueryHandler(IWebServiceNotificationContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<WebServiceNotifierDto>> Handle(GetWebServiceNotifierQuery request, CancellationToken cancellationToken)
        {
            var notifier =
                await _context.Notifiers.SingleOrDefaultAsync(x => !x.IsDeleted && x.Id == request.Id,
                    cancellationToken);

            if (notifier is null)
                return Result<WebServiceNotifierDto>.Failed(new NotFoundObjectResult(new ApiMessage(ResponseMessage.NotifierNotFound)));

            if (notifier.ServiceType != ServiceType.WebService)
                return Result<WebServiceNotifierDto>.Failed(new BadRequestObjectResult(new ApiMessage(ResponseMessage.WrongApiCall)));


            return Result<WebServiceNotifierDto>.SuccessFul(_mapper.Map<WebServiceNotifierDto>(notifier));
        }
    }
}