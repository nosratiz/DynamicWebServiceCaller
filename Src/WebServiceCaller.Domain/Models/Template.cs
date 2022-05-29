using System;

namespace WebServiceCaller.Domain.Models
{
    public class Template
    {
        public long Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public bool HasHtml { get; set; }

        public string Tags { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifiedDate { get; set; }

        public bool IsDeleted { get; set; }
    }
}