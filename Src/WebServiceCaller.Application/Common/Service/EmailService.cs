using System;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using WebServiceCaller.Application.Common.Interfaces;
using WebServiceCaller.Common.Options;

namespace WebServiceCaller.Application.Common.Service
{
    public class EmailService : IEmailService
    {
        public async Task<(bool, string)> SendMessage(string emailTo, string title, string content, 
            bool isHtml, EmailSetting emailSetting)
        {
            bool isSend = true;
            string logMessage = "";
            MailMessage message = new MailMessage(emailSetting.From, emailTo, title, content)
            {
                IsBodyHtml = isHtml,
                BodyEncoding = Encoding.UTF8,
                SubjectEncoding = Encoding.Default
            };

            NetworkCredential credential = new NetworkCredential(emailSetting.UserName, emailSetting.Password);

            SmtpClient smtp = new SmtpClient(emailSetting.Host, emailSetting.Port)
            {
                Credentials = credential,
                EnableSsl = true
            };

            try
            {
                await smtp.SendMailAsync(message);
                smtp.Dispose();
                logMessage = $"پیام با موفیقت به {emailTo} ارسال شد";
            }
            catch (Exception ex)
            {
                //
                isSend = false;
                logMessage = ex.Message;
            }

            return (isSend, logMessage);
        }
    }
}