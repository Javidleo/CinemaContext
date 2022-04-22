using DomainModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Mapping
{
    public class SalonMapping : IEntityTypeConfiguration<Salon>
    {
        public void Configure(EntityTypeBuilder<Salon> builder)
        {
            builder.Property(i => i.SalonGuid).ValueGeneratedOnAdd();

            builder
                .HasOne(i => i.Cinema)
                .WithMany(i => i.Salons)
                .HasForeignKey(i => i.CinemaId);
        }
    }
}
