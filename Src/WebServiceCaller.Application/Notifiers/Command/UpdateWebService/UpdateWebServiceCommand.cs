using System.Collections.Generic;
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
using WebServiceCaller.Common.Result;
using WebServiceCaller.Domain.Enum;

namespace WebServiceCaller.Application.Notifiers.Command.UpdateWebService
{
    public class UpdateWebServiceCommand : IRequest<Result<WebServiceNotifierDto>>
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public string Url { get; set; }

        public string Body { get; set; }

        public ContentType ContentType { get; set; }

        public HttpMethod Method { get; set; }

        public List<Header> Headers { get; set; }
    }

    public class UpdateWebServiceCommandHandler : IRequestHandler<UpdateWebServiceCommand, Result<WebServiceNotifierDto>>
    {
        private readonly IWebServiceNotificationContext _context;
        private readonly IMapper _mapper;

        public UpdateWebServiceCommandHandler(IWebServiceNotificationContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<WebServiceNotifierDto>> Handle(UpdateWebServiceCommand request, CancellationToken cancellationToken)
        {
            var notifier =
                await _context.Notifiers.SingleOrDefaultAsync(x => !x.IsDeleted && x.Id == request.Id,
                    cancellationToken);

            if (notifier is null)
                return Result<WebServiceNotifierDto>.Failed(new NotFoundObjectResult(new ApiMessage(ResponseMessage.NotifierNotFound)));

            _mapper.Map(request, notifier);

            notifier.Setting = JsonConvert.SerializeObject(new WebServiceSetting
            {
                Url = request.Url,
                Body = request.Body,
                ContentType = request.ContentType,
                Headers = request.Headers,
                Method = request.Method
            });

            await _context.SaveAsync(cancellationToken);

            return Result<WebServiceNotifierDto>.SuccessFul(_mapper.Map<WebServiceNotifierDto>(notifier));
        }
    }
}