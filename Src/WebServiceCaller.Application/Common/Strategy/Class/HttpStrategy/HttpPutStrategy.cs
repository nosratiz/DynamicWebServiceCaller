using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using Serilog;
using WebServiceCaller.Application.Common.Strategy.Interfaces;
using WebServiceCaller.Application.Notifiers.ModelDto;
using WebServiceCaller.Common.Helper;
using WebServiceCaller.Common.Options;
using WebServiceCaller.Domain.Enum;

namespace WebServiceCaller.Application.Common.Strategy.Class.HttpStrategy
{
    public class HttpPutStrategy : IHttpStrategy
    {
        public async Task<IRestResponse> ExecuteRequest(string to, string content, List<Tag> tagValues, WebServiceSetting webServiceSetting)
        {
            var url = Utils.InterpolateTags(webServiceSetting.Url, tagValues);

            var client = new RestClient(url);

            var request = new RestRequest { Method = Method.PUT };

            #region Set body If User Set

            if (!string.IsNullOrWhiteSpace(webServiceSetting.Body))
            {
                var body = Utils.InterpolateTags(webServiceSetting.Body, tagValues);

                if (webServiceSetting.ContentType == ContentType.ApplicationJson)
                {
                    request.AddParameter("application/json", body
                        , ParameterType.RequestBody);
                }
         
                else
                {
                    try
                    {
                        var formBody = JsonConvert.DeserializeObject<Dictionary<string, string>>(body);

                        foreach (KeyValuePair<string, string> tagValue in formBody)
                            request.AddParameter(tagValue.Key, tagValue.Value);
                    }
                    catch (Exception ex)
                    {
                        Log.Information(ex.Message);
                        // ignored
                    }
                }
            }

            #endregion Set body If User Set

            webServiceSetting.Headers.ForEach(x =>
            {
                request.AddHeader(x.Key, x.Value);
            });

            return await client.ExecuteAsync(request);
        }
    }
}