using DomainModel.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Mapping
{
    public class ChairActivityMapping : IEntityTypeConfiguration<ChairActivity>
    {
        public void Configure(EntityTypeBuilder<ChairActivity> builder)
        {
            builder.Property(i => i.ChairActivityGuid).ValueGeneratedOnAdd();

            builder
                .HasOne(i => i.Chair)
                .WithMany(i => i.ChairActivities)
                .HasForeignKey(i => i.ChairId);
        }
    }
}
