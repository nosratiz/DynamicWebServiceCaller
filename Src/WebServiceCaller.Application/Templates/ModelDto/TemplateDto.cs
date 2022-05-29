using System;
using System.Collections.Generic;

namespace WebServiceCaller.Application.Templates.ModelDto
{
    public class TemplateDto
    {
        public long Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public bool HasHtml { get; set; }

        public List<string> Tags { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}