using System;
using WebServiceCaller.Domain.Enum;

namespace WebServiceCaller.Domain.Models
{
    public class NotifierLog
    {
        public long Id { get; set; }
        public long NotifierId { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public LogStatus logStatus { get; set; }

        public virtual Notifier Notifier { get; set; }
    }
}