using Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Data.Mapping
{
  public class UserMap : IEntityTypeConfiguration<UserEntity>
  {
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
      builder.ToTable("User");

      builder.HasKey(p => p.Id);

      builder.HasIndex(p => p.Email)
             .IsUnique();

      builder.Property(p => p.Email)
             .HasMaxLength(100);

      builder.Property(u => u.Name)
             .IsRequired()
             .HasMaxLength(60);
    }
  }
}
