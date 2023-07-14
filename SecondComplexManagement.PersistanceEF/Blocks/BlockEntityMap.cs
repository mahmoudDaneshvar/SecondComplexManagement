using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SecondComplexManagement.Entities;

namespace SecondComplexManagement.PersistanceEF.Blocks
{
    public class BlockEntityMap : IEntityTypeConfiguration<Block>
    {
        public void Configure(EntityTypeBuilder<Block> entity)
        {
            entity.ToTable("Blocks");
            entity.HasKey(_ => _.Id);
            entity.Property(_ => _.Id).ValueGeneratedOnAdd();
            entity.Property(_ => _.Name).IsRequired().HasMaxLength(225);
            entity.Property(_ => _.ComplexId).IsRequired().ValueGeneratedNever();
            entity.Property(_ => _.UnitCount).IsRequired().ValueGeneratedNever();

            entity.HasOne(_ => _.Complex)
                .WithMany(_ => _.Blocks)
                .HasForeignKey(_ => _.ComplexId).OnDelete(DeleteBehavior.Cascade);

            

        }
    }
}
