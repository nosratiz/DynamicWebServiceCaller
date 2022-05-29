using System;
using WebServiceCaller.Domain.Enum;

namespace WebServiceCaller.Application.Notifiers.ModelDto
{
    public class NotifierListDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public ServiceType ServiceType { get; set; }
        public DateTime CreateDate { get; set; }
    }
}