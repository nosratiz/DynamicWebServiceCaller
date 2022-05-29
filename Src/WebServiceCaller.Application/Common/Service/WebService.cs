using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebServiceCaller.Application.Common.Interfaces;
using WebServiceCaller.Application.Common.Strategy;
using WebServiceCaller.Application.Notifiers.ModelDto;
using WebServiceCaller.Common.Options;

namespace WebServiceCaller.Application.Common.Service
{
    public class WebService : IWebService
    {
        public async Task<(bool, string)> SendMessage(string to, string content, List<Tag> tagValues, WebServiceSetting webServiceSetting)
        {
            var isValid = true;
            string logMessage = "";

            try
            {
                var context = new RequestStrategyContext();

                context.DetectStrategy(webServiceSetting.Method);

                var response = await context.ExecuteRequest(to, content, tagValues, webServiceSetting);

                logMessage = response.Content;
            }
            catch (Exception e)
            {
                isValid = false;
                logMessage = e.Message;
            }

            return (isValid, logMessage);
        }
    }
}