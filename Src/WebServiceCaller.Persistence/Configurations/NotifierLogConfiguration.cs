using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebServiceCaller.Domain.Models;

namespace WebServiceCaller.Persistence.Configurations
{
    public class NotifierLogConfiguration : IEntityTypeConfiguration<NotifierLog>
    {
        public void Configure(EntityTypeBuilder<NotifierLog> builder)
        {
            builder.Property(e => e.Id).HasColumnName("NotifierLogId").IsRequired().UseIdentityColumn();

            builder.HasKey(e => e.Id);

            builder.Property(e => e.CreateDate).IsRequired();

            builder.Property(e => e.logStatus).IsRequired();

            builder.Property(e => e.Description).IsRequired();

            builder.HasOne(e => e.Notifier).WithMany(e => e.NotifierLogs).HasForeignKey(e => e.NotifierId).IsRequired().OnDelete(DeleteBehavior.Cascade);
        }
    }
}