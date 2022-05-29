using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebServiceCaller.Domain.Models;

namespace WebServiceCaller.Persistence.Configurations
{
    public class TemplateConfiguration : IEntityTypeConfiguration<Template>
    {
        public void Configure(EntityTypeBuilder<Template> builder)
        {
            builder.Property(e => e.Id).HasColumnName("TemplateId").IsRequired().UseIdentityColumn();

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Title).IsRequired();

            builder.Property(e => e.Content).IsRequired();

            builder.Property(e => e.HasHtml).IsRequired();

            builder.Property(e => e.CreateDate).IsRequired();

            builder.Property(e => e.ModifiedDate).IsRequired();
        }
    }
}