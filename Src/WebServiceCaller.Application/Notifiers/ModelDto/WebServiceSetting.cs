using System.Collections.Generic;
using WebServiceCaller.Domain.Enum;

namespace WebServiceCaller.Application.Notifiers.ModelDto
{
    public class WebServiceSetting
    {
        public string Url { get; set; }

        public string Body { get; set; }

        public ContentType ContentType { get; set; }

        public HttpMethod Method { get; set; }

        public List<Header> Headers { get; set; }
    }

    public class Header
    {
        public string Key { get; set; }

        public string Value { get; set; }
    }

    public class Body
    {
        public string Key { get; set; }

        public string Value { get; set; }
    }
}