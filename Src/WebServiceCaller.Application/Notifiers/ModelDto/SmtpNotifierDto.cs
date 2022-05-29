using System;
using WebServiceCaller.Common.Options;
using WebServiceCaller.Domain.Enum;

namespace WebServiceCaller.Application.Notifiers.ModelDto
{
    public class SmtpNotifierDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public ServiceType ServiceType { get; set; }
        public EmailSetting Setting { get; set; }
        public DateTime CreateDate { get; set; }
    }
}