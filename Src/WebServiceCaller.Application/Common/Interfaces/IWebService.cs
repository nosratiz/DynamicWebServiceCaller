using System.Collections.Generic;
using System.Threading.Tasks;
using WebServiceCaller.Application.Notifiers.ModelDto;
using WebServiceCaller.Common.Options;

namespace WebServiceCaller.Application.Common.Interfaces
{
    public interface IWebService
    {
        Task<(bool, string)> SendMessage(string to, string content, List<Tag> tagValues, WebServiceSetting webServiceSetting);
    }
}