using DomainModel.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Mapping
{
    public class SalonActivityMapping : IEntityTypeConfiguration<SalonActivity>
    {
        public void Configure(EntityTypeBuilder<SalonActivity> builder)
        {
            builder.Property(i => i.SalonActivityGuid).ValueGeneratedOnAdd();

            builder
                .HasOne(i => i.Salon)
                .WithMany(i => i.SalonActivities)
                .HasForeignKey(i => i.SalonId);
        }
    }
}
