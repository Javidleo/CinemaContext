using DomainModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Mapping
{
    public class MovieSansSalonMapping : IEntityTypeConfiguration<MovieSansSalon>
    {
        public void Configure(EntityTypeBuilder<MovieSansSalon> builder)
        {
            builder
                .HasOne(i => i.Salon)
                .WithMany(i => i.MovieSansSalons)
                .HasForeignKey(i => i.SalonId);
            builder
                .HasOne(i=> i.Movie)
                .WithMany(i=> i.MovieSansSalons)
                .HasForeignKey(i=> i.MovieId);
            builder
                .HasOne(i => i.Sans)
                .WithMany(i => i.MovieSansSalons)
                .HasForeignKey(i => i.SansId);
        }
    }
}
