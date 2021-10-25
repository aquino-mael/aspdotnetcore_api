using Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Data.Mapping
{
    public class ZipCodeMap : IEntityTypeConfiguration<ZipCodeEntity>
    {
        public void Configure(EntityTypeBuilder<ZipCodeEntity> builder)
        {
            builder.ToTable("ZipCode");

            builder.HasKey(zip => zip.Id);

            builder.HasIndex(zip => zip.ZipCode);

            builder.HasOne<CountyEntity>(zip => zip.County)
                   .WithMany(county => county.ZipCodes);
        }
    }
}
