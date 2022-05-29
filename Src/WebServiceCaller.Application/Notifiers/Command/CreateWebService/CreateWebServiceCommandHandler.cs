using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Newtonsoft.Json;
using WebServiceCaller.Application.Common.Interfaces;
using WebServiceCaller.Application.Notifiers.ModelDto;
using WebServiceCaller.Common.Result;
using WebServiceCaller.Domain.Models;

namespace WebServiceCaller.Application.Notifiers.Command.CreateWebService
{
    public class CreateWebServiceCommandHandler : IRequestHandler<CreateWebServiceCommand, Result<SmtpNotifierDto>>
    {
        private readonly IWebServiceNotificationContext _context;
        private readonly IMapper _mapper;

        public CreateWebServiceCommandHandler(IWebServiceNotificationContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<SmtpNotifierDto>> Handle(CreateWebServiceCommand request, CancellationToken cancellationToken)
        {
            var notifier = _mapper.Map<Notifier>(request);

            notifier.Setting = JsonConvert.SerializeObject(new WebServiceSetting
            {
                Url = request.Url,
                Body = request.Body,
                ContentType = request.ContentType,
                Headers = request.Headers,
                Method = request.Method
            });

            await _context.Notifiers.AddAsync(notifier, cancellationToken);

            await _context.SaveAsync(cancellationToken);

            return Result<SmtpNotifierDto>.SuccessFul(_mapper.Map<SmtpNotifierDto>(notifier));
        }
    }
}