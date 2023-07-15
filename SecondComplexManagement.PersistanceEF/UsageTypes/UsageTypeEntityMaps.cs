using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SecondComplexManagement.Entities;

namespace SecondComplexManagement.PersistanceEF.UsageTypes
{
    internal class UsageTypeEntityMaps : IEntityTypeConfiguration<UsageType>
    {
        public void Configure(EntityTypeBuilder<UsageType> entity)
        {
            entity.ToTable("UsageTypes");
            entity.HasKey(_ => _.Id);
            entity.Property(_ => _.Id).ValueGeneratedOnAdd();
            entity.Property(_ => _.Name).IsRequired().HasMaxLength(225);
        }
    }
}
