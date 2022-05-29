using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebServiceCaller.Domain.Models;

namespace WebServiceCaller.Application.Common.Interfaces
{
    public interface IWebServiceNotificationContext
    {
        DbSet<Notifier> Notifiers { get; set; }

        DbSet<NotifierLog> NotifierLogs { get; set; }

        DbSet<Template> Templates { get; set; }

        Task SaveAsync(CancellationToken cancellationToken);
    }
}