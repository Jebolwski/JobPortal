using JobPortal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Net;

namespace JobPortal.Data.Mapping
{
    public class EmployerMap : IEntityTypeConfiguration<Employer>
    {
        public void Configure(EntityTypeBuilder<Employer> builder)
        {
            builder.ToTable("Employer");
            builder.Property(p => p.id).HasColumnName("id");
            builder.Property(p => p.company_date_created).HasColumnName("company_date_created");
            builder.Property(p => p.company_logo_photo_url).HasColumnName("company_logo_photo_url");
            builder.Property(p => p.company_name).HasColumnName("company_name");
            builder.Property(p => p.companys_job).HasColumnName("companys_job");
        }
    }
}
