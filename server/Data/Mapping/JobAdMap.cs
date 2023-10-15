using JobPortal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Net;

namespace JobPortal.Data.Mapping
{
    public class JobAdMap : IEntityTypeConfiguration<JobAd>
    {
        public void Configure(EntityTypeBuilder<JobAd> builder)
        {
            builder.ToTable("JobAd");
            builder.Property(p => p.id).HasColumnName("id");
            builder.Property(p => p.title).HasColumnName("title");
            builder.Property(p => p.description).HasColumnName("description");
            builder.Property(p => p.creator_id).HasColumnName("creator_id");
            builder.HasMany(p => p.photos).WithOne().HasForeignKey(p => p.jobAdId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
