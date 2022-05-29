using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebServiceCaller.Application.Common.Interfaces;
using WebServiceCaller.Domain.Models;

namespace WebServiceCaller.Persistence.Context
{
    public class WebServiceNotificationContext : DbContext, IWebServiceNotificationContext
    {
        public WebServiceNotificationContext()
        {
        }

        public WebServiceNotificationContext(DbContextOptions<WebServiceNotificationContext> options) : base(options)
        {
        }

        public virtual DbSet<Notifier> Notifiers { get; set; }

        public virtual DbSet<NotifierLog> NotifierLogs { get; set; }

        public virtual DbSet<Template> Templates { get; set; }
        public Task SaveAsync(CancellationToken cancellationToken) => base.SaveChangesAsync(cancellationToken);


        protected override void OnModelCreating(ModelBuilder modelBuilder) => modelBuilder.ApplyConfigurationsFromAssembly(typeof(WebServiceNotificationContext).Assembly);
    }
}