using DomainModel.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Mapping
{
    public class SansMapping : IEntityTypeConfiguration<Sans>
    {
        public void Configure(EntityTypeBuilder<Sans> builder)
        {
            builder.Property(i => i.SansGuid).ValueGeneratedOnAdd();
        }
    }
}
