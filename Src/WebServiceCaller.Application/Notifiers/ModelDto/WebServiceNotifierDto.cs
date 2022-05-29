using System;
using WebServiceCaller.Domain.Enum;

namespace WebServiceCaller.Application.Notifiers.ModelDto
{
    public class WebServiceNotifierDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public ServiceType ServiceType { get; set; }
        public WebServiceSetting Setting { get; set; }
        public DateTime CreateDate { get; set; }
    }
}