using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebServiceCaller.Application.Common.Interfaces;
using WebServiceCaller.Application.Notifiers.ModelDto;
using WebServiceCaller.Common.General;
using WebServiceCaller.Common.Options;
using WebServiceCaller.Common.Result;

namespace WebServiceCaller.Application.Notifiers.Command.UpdateSmtpNotifier
{
    public class UpdateSmtpNotifierCommandHandler : IRequestHandler<UpdateSmtpNotifierCommand, Result<SmtpNotifierDto>>
    {
        private readonly IWebServiceNotificationContext _context;
        private readonly IMapper _mapper;

        public UpdateSmtpNotifierCommandHandler(IWebServiceNotificationContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<SmtpNotifierDto>> Handle(UpdateSmtpNotifierCommand request, CancellationToken cancellationToken)
        {
            var notifier = await _context.Notifiers.SingleOrDefaultAsync(x =>
                !x.IsDeleted && x.Id == request.Id, cancellationToken);

            if (notifier is null)
                return Result<SmtpNotifierDto>.Failed(new NotFoundObjectResult(new ApiMessage(ResponseMessage.NotifierNotFound)));

            _mapper.Map(request, notifier);

            notifier.Setting = JsonConvert.SerializeObject(new EmailSetting
            {
                EnableSsl = request.EnableSsl,
                From = request.From,
                Host = request.Host,
                Password = request.Password,
                Port = request.Port,
                UserName = request.UserName
            });

            await _context.SaveAsync(cancellationToken);

            return Result<SmtpNotifierDto>.SuccessFul(_mapper.Map<SmtpNotifierDto>(notifier));
        }
    }
}