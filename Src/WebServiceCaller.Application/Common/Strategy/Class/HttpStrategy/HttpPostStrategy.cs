using System;
using System.Collections.Generic;
using System.Text;
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
    public class HttpPostStrategy : IHttpStrategy
    {
        public async Task<IRestResponse> ExecuteRequest(string to, string content, List<Tag> tagValues, WebServiceSetting webServiceSetting)
        {
            //if url contain query string and need be dynamic
            var url = Utils.InterpolateTags(webServiceSetting.Url, tagValues);

            //interpolate template message that user set
            string message = Utils.InterpolateTags(content, tagValues);

            byte[] messageUtf8Bytes = Encoding.UTF8.GetBytes(message);//Encoding.Unicode.GetString(utf8Bytes);

            var stringcleanMessage = Encoding.UTF8.GetString(messageUtf8Bytes);

            var client = new RestClient(url);

            var request = new RestRequest { Method = Method.POST };

            #region Set body If User Set

            if (!string.IsNullOrWhiteSpace(webServiceSetting.Body))
            {
                var body = Utils.InterpolateTags(webServiceSetting.Body, tagValues);

                body = Utils.InterpolateMessage(body, stringcleanMessage, to);

                if (webServiceSetting.ContentType == ContentType.ApplicationJson)
                    request.AddParameter("application/json", body, ParameterType.RequestBody);

                else if (webServiceSetting.ContentType == ContentType.FormUrlEncode)
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

                else
                    request.AddParameter("application/xml", body, ParameterType.RequestBody);

            }

            #endregion Set body If User Set

            //set headers
            webServiceSetting.Headers.ForEach(x =>
            {
                request.AddHeader(x.Key, x.Value);
            });

            return await client.ExecuteAsync(request);
        }
    }
}