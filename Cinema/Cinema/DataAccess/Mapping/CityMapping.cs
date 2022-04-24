using DomainModel.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Mapping
{
    public class CityMapping : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.Property(i => i.CityGuid).ValueGeneratedOnAdd();

            builder
                .HasOne(i => i.Province)
                .WithMany(i => i.Cities)
                .HasForeignKey(i => i.ProvinceId);
        }
    }
}
