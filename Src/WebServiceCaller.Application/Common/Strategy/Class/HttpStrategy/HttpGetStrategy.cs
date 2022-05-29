using System.Collections.Generic;
using System.Threading.Tasks;
using RestSharp;
using WebServiceCaller.Application.Common.Strategy.Interfaces;
using WebServiceCaller.Application.Notifiers.ModelDto;
using WebServiceCaller.Common.Helper;
using WebServiceCaller.Common.Options;

namespace WebServiceCaller.Application.Common.Strategy.Class.HttpStrategy
{
    public class HttpGetStrategy : IHttpStrategy
    {
        public async Task<IRestResponse> ExecuteRequest(string to, string content, List<Tag> tagValues, WebServiceSetting webServiceSetting)
        {
            var url = Utils.InterpolateTags(webServiceSetting.Url, tagValues);

            var client = new RestClient(url);

            var request = new RestRequest { Method = Method.GET };

            webServiceSetting.Headers.ForEach(x =>
            {
                request.AddHeader(x.Key, x.Value);
            });

            return await client.ExecuteAsync(request);
        }
    }
}