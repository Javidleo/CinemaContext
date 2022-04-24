using DomainModel.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Mapping
{
    public class TicketMapping : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.Property(i => i.TicketGuid).ValueGeneratedOnAdd();

            builder
                .HasOne(i => i.Chair)
                .WithMany(i => i.Tickets)
                .HasForeignKey(i => i.ChairId);
            builder
                .HasOne(i => i.Salon)
                .WithMany(i => i.Tickets)
                .HasForeignKey(i => i.SalonId);
            builder
                .HasOne(i => i.Cinema)
                .WithMany(i => i.Tickets)
                .HasForeignKey(i => i.CinemaId);
        }
    }
}
