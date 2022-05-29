using System.Collections.Generic;
using System.Text;
using WebServiceCaller.Common.Options;

namespace WebServiceCaller.Common.Helper
{
    public static class Utils
    {
        public static string InterpolateTags(string templateMessage, List<string> key, List<string> value)
        {
            StringBuilder content = new StringBuilder(templateMessage);

            for (int i = 0; i < value.Count; i++)
                content.Replace($"%{key[i]}%", value[i]);

            return content.ToString();
        }

        public static string InterpolateTags(string templateMessage, List<Tag> tagValues)
        {
            StringBuilder content = new StringBuilder(templateMessage);

            foreach (var value in tagValues)
                content.Replace($"%{value.Key}%", value.Value);

            return content.ToString();
        }

        public static string InterpolateMessage(string message, string templateContent, string to)
        {
            StringBuilder content = new StringBuilder(message);

            content = content.Replace("%content%", templateContent);

            content = content.Replace("%to%", to);

            return content.ToString();
        }
    }
}