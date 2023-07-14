

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SecondComplexManagement.Entities;

namespace SecondComplexManagement.PersistanceEF.Units
{
    internal class UnitEntityMap : IEntityTypeConfiguration<Unit>
    {
        public void Configure(EntityTypeBuilder<Unit> entity)
        {
            entity.ToTable("Units");
            entity.HasKey(_ => _.Id);
            entity.Property(_ => _.Id).ValueGeneratedOnAdd();
            entity.Property(_ => _.ResidenceType).IsRequired();
            entity.Property(_ => _.Name).IsRequired().HasMaxLength(225);
            entity.Property(_ => _.BlockId).IsRequired().ValueGeneratedNever();

            entity.HasOne(_ => _.Block)
                .WithMany(_ => _.Units)
                .HasForeignKey(_ => _.BlockId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
