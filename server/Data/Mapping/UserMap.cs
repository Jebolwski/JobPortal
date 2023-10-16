using JobPortal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobPortal.Data.Mapping
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");
            builder.Property(p => p.id).HasColumnName("id");
            builder.Property(p => p.email).HasColumnName("email");
            builder.Property(p => p.name).HasColumnName("name");
            builder.Property(p => p.firstName).HasColumnName("firstName");
            builder.Property(p => p.lastName).HasColumnName("lastName");
            builder.Property(p => p.RefreshToken).HasColumnName("refreshToken");
            builder.Property(p => p.TokenCreated).HasColumnName("tokenCreated");
            builder.Property(p => p.passwordHash).HasColumnName("passwordHash");
            builder.Property(p => p.passwordSalt).HasColumnName("passwordSalt");
            builder.Property(p => p.TokenExpires).HasColumnName("tokenExpires");
            builder.Property(p => p.roleId).HasColumnName("roleId");
        }
    }
}
