using DomainModel.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Mapping
{
    public class CinemaMapping : IEntityTypeConfiguration<Cinema>
    {
        public void Configure(EntityTypeBuilder<Cinema> builder)
        {
            builder.Property(i => i.CinemaGuid).ValueGeneratedOnAdd();

            builder
                .HasOne(i => i.City)
                .WithMany(i => i.Cinemas)
                .HasForeignKey(i => i.CityId);
        }
    }
}
