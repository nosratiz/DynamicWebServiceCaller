using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Newtonsoft.Json;
using WebServiceCaller.Application.Common.Interfaces;
using WebServiceCaller.Application.Notifiers.ModelDto;
using WebServiceCaller.Common.Options;
using WebServiceCaller.Common.Result;
using WebServiceCaller.Domain.Models;

namespace WebServiceCaller.Application.Notifiers.Command.CreateSmtpNotifier
{
    public class CreateSmtpNotifierCommandHandler : IRequestHandler<CreateSmtpNotifierCommand, Result<SmtpNotifierDto>>
    {
        private readonly IWebServiceNotificationContext _context;
        private readonly IMapper _mapper;

        public CreateSmtpNotifierCommandHandler(IWebServiceNotificationContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<SmtpNotifierDto>> Handle(CreateSmtpNotifierCommand request, CancellationToken cancellationToken)
        {
            var notifier = _mapper.Map<Notifier>(request);

            notifier.Setting = JsonConvert.SerializeObject(new EmailSetting
            {
                EnableSsl = request.EnableSsl,
                From = request.From,
                Host = request.Host,
                Password = request.Password,
                Port = request.Port,
                UserName = request.UserName
            });

            await _context.Notifiers.AddAsync(notifier, cancellationToken);

            await _context.SaveAsync(cancellationToken);

            return Result<SmtpNotifierDto>.SuccessFul(_mapper.Map<SmtpNotifierDto>(notifier));
        }
    }
}