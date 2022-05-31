using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebServiceCaller.Application.Common.Interfaces;
using WebServiceCaller.Common.Options;
using WebServiceCaller.Domain.Enum;
using WebServiceCaller.Domain.Models;

namespace WebServiceCaller.Application.System
{
    public class SeedData
    {
        private readonly IWebServiceNotificationContext _context;

        public SeedData(IWebServiceNotificationContext Context)
        {
            _context = Context;
        }

        public async Task SeedAllAsync(CancellationToken cancellationToken)
        {
            if (!await _context.Notifiers.AnyAsync(cancellationToken))
            {
                await _context.Notifiers.AddAsync(new Notifier
                {
                    Name = "Nosrati",
                    CreateDate = DateTime.Now,
                    ServiceType = ServiceType.Smtp,
                    Setting = JsonConvert.SerializeObject(new EmailSetting
                    {
                        From = "yourmail@hotmail.com",
                        Port = 587,
                        UserName = "yourmail@hotmail.com",
                        Password = "yourPassword",
                        Host = "smtp.live.com",
                        EnableSsl = true
                    })
                }, cancellationToken);

                await _context.SaveAsync(cancellationToken);
            }
        }
    }
}