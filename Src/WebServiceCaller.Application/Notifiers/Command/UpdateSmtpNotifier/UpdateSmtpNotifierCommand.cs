using MediatR;
using WebServiceCaller.Application.Notifiers.ModelDto;
using WebServiceCaller.Common.Result;

namespace WebServiceCaller.Application.Notifiers.Command.UpdateSmtpNotifier
{
    public class UpdateSmtpNotifierCommand : IRequest<Result<SmtpNotifierDto>>
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string From { get; set; }

        public string Host { get; set; }

        public int Port { get; set; }

        public bool EnableSsl { get; set; }
    }
}