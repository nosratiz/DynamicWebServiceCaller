using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RestSharp;
using Serilog;
using WebServiceCaller.Application.Common.Strategy.Class.HttpStrategy;
using WebServiceCaller.Application.Common.Strategy.Interfaces;
using WebServiceCaller.Application.Notifiers.ModelDto;
using WebServiceCaller.Common.Options;
using WebServiceCaller.Domain.Enum;

namespace WebServiceCaller.Application.Common.Strategy
{
    public class RequestStrategyContext
    {
        private IHttpStrategy _httpStrategy;

        public void SetStrategy(IHttpStrategy httpStrategy)
        {
            _httpStrategy = httpStrategy;
        }

        public IHttpStrategy DetectStrategy(HttpMethod method)
        {
            switch (method)
            {
                case HttpMethod.Get:
                    return _httpStrategy = new HttpGetStrategy();

                case HttpMethod.Delete:
                    return _httpStrategy = new HttpDeleteStrategy();

                case HttpMethod.Post:
                    return _httpStrategy = new HttpPostStrategy();

                case HttpMethod.Put:
                    return _httpStrategy = new HttpPutStrategy();

                default:
                    Log.Information("http method  Argument out of range Exception");
                    throw new ArgumentOutOfRangeException();
            }
        }
         
        public async Task<IRestResponse> ExecuteRequest(string to, string content, List<Tag> tagValues, WebServiceSetting webServiceSetting)
            => await _httpStrategy.ExecuteRequest(to, content, tagValues, webServiceSetting);
    }
}