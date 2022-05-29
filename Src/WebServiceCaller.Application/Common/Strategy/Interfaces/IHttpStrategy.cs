using System.Collections.Generic;
using System.Threading.Tasks;
using RestSharp;
using WebServiceCaller.Application.Notifiers.ModelDto;
using WebServiceCaller.Common.Options;

namespace WebServiceCaller.Application.Common.Strategy.Interfaces
{
    public interface IHttpStrategy
    {
        Task<IRestResponse> ExecuteRequest(string to, string content, List<Tag> tagValues, WebServiceSetting webServiceSetting);
    }
}