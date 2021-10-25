using Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Mapping
{
    public class CountyMap : IEntityTypeConfiguration<CountyEntity>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<CountyEntity> builder)
        {
            builder.ToTable("Municipio");

            builder.HasKey(uf => uf.Id);

            builder.HasIndex(uf => uf.IBGECode);

            builder.HasOne(uf => uf.Uf)
                   .WithMany(m => m.Counties);
        }
    }
}
