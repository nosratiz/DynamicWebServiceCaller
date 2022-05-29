using System.Collections.Generic;
using MediatR;
using WebServiceCaller.Application.Notifiers.ModelDto;
using WebServiceCaller.Common.Result;
using WebServiceCaller.Domain.Enum;

namespace WebServiceCaller.Application.Notifiers.Command.CreateWebService
{
    public class CreateWebServiceCommand : IRequest<Result<SmtpNotifierDto>>
    {
        public string Name { get; set; }

        public string Url { get; set; }

        public string Body { get; set; }

        public ContentType ContentType { get; set; }

        public HttpMethod Method { get; set; }

        public List<Header> Headers { get; set; }
    }
}