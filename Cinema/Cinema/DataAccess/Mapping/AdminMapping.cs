using DomainModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Mapping
{
    public class AdminMapping : IEntityTypeConfiguration<Admin>
    {
        public void Configure(EntityTypeBuilder<Admin> builder)
        {
            builder.Property(i => i.AdminGuid).ValueGeneratedOnAdd();

            builder
                .HasOne(i => i.Cinema)
                .WithMany(i => i.Admins)
                .HasForeignKey(i => i.CinemaId);
        }
    }
}
