using JobPortal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobPortal.Data.Mapping
{
    public class JobAdPhotoMap : IEntityTypeConfiguration<JobAdPhoto>
    {
        public void Configure(EntityTypeBuilder<JobAdPhoto> builder)
        {
            builder.ToTable("JobAdPhoto");
            builder.Property(p => p.id).HasColumnName("id");
            builder.Property(p => p.photoUrl).HasColumnName("photoUrl");
            builder.Property(p => p.jobAdId).HasColumnName("jobAdId");
        }
    }
}
