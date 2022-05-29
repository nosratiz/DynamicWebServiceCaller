using System;
using System.Collections.Generic;
using WebServiceCaller.Domain.Enum;

namespace WebServiceCaller.Domain.Models
{
    public class Notifier
    {
        public Notifier()
        {
            NotifierLogs = new HashSet<NotifierLog>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public ServiceType ServiceType { get; set; }
        public string Setting { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreateDate { get; set; }

        public virtual ICollection<NotifierLog> NotifierLogs { get; }
    }
}