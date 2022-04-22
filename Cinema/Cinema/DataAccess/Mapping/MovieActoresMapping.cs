using DomainModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Mapping
{
    public class MovieActoresMapping : IEntityTypeConfiguration<MovieActores>
    {
        public void Configure(EntityTypeBuilder<MovieActores> builder)
        {
            builder.Property(i => i.MovieActorGuid).ValueGeneratedOnAdd();

            builder
                .HasOne(i => i.Movie)
                .WithMany(i => i.MovieActores)
                .HasForeignKey(i => i.MovieId);
        }
    }
}
