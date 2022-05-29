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
    public class GetSmtpNotifierQuery : IRequest<Result<SmtpNotifierDto>>
    {
        public long Id { get; set; }
    }

    public class GetNotifierQueryHandler : IRequestHandler<GetSmtpNotifierQuery, Result<SmtpNotifierDto>>
    {
        private readonly IWebServiceNotificationContext _context;
        private readonly IMapper _mapper;

        public GetNotifierQueryHandler(IWebServiceNotificationContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<SmtpNotifierDto>> Handle(GetSmtpNotifierQuery request, CancellationToken cancellationToken)
        {
            var notifier =
                await _context.Notifiers.SingleOrDefaultAsync(x => !x.IsDeleted && x.Id == request.Id,
                    cancellationToken);

            if (notifier is null)
                return Result<SmtpNotifierDto>.Failed(new NotFoundObjectResult(new ApiMessage(ResponseMessage.NotifierNotFound)));

            if (notifier.ServiceType != ServiceType.Smtp)
                return Result<SmtpNotifierDto>.Failed(new BadRequestObjectResult(new ApiMessage(ResponseMessage.WrongApiCall)));
            

            return Result<SmtpNotifierDto>.SuccessFul(_mapper.Map<SmtpNotifierDto>(notifier));
        }
    }
}