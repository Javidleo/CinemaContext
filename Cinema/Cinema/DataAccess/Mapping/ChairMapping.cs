using DomainModel.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Mapping

{
    internal class ChairMapping : IEntityTypeConfiguration<Chair>
    {
        public void Configure(EntityTypeBuilder<Chair> builder)
        {
            builder.Property(i => i.ChairGuid).ValueGeneratedOnAdd();

            builder
                .HasOne(i => i.Salon)
                .WithMany(i => i.Chairs)
                .HasForeignKey(i => i.SalonId);
        }
    }
}
