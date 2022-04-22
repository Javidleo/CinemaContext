using DomainModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Mapping
{
    public class CinemaActivityMapping : IEntityTypeConfiguration<CinemaActivity>
    {
        public void Configure(EntityTypeBuilder<CinemaActivity> builder)
        {
            builder.Property(i => i.CinemaActivityGuid).ValueGeneratedOnAdd();

            builder
                .HasOne(i => i.Cinema)
                .WithMany(i => i.CinemaActivities)
                .HasForeignKey(i => i.CinemaId);
        }
    }
}
