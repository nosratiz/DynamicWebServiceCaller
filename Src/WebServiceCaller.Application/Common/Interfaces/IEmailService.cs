using System.Threading.Tasks;
using WebServiceCaller.Common.Options;

namespace WebServiceCaller.Application.Common.Interfaces
{
    public interface IEmailService
    {
        Task<(bool, string)> SendMessage(string emailTo, string title, string content, bool isHtml, EmailSetting emailSetting);
    }
}