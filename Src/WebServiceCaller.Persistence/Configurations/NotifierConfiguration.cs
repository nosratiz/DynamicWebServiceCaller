using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebServiceCaller.Domain.Models;

namespace WebServiceCaller.Persistence.Configurations
{
    public class NotifierConfiguration : IEntityTypeConfiguration<Notifier>
    {
        public void Configure(EntityTypeBuilder<Notifier> builder)
        {
            builder.Property(e => e.Id).HasColumnName("NotifierId").IsRequired().UseIdentityColumn();

            builder.HasKey(e => e.Id);

            builder.Property(e => e.ServiceType).IsRequired();

            builder.Property(e => e.Name).IsRequired();

            builder.Property(e => e.Setting).IsRequired();

            builder.Property(e => e.IsDeleted).IsRequired().HasDefaultValue(false);

            builder.Property(e => e.CreateDate).IsRequired();
        }
    }
}