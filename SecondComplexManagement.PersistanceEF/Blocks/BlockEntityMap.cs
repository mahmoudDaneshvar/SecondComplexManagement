using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SecondComplexManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondComplexManagement.PersistanceEF.Blocks
{
    internal class BlockEntityMap : IEntityTypeConfiguration<Block>
    {
        public void Configure(EntityTypeBuilder<Block> entity)
        {
            entity.ToTable("Blocks");
            entity.HasKey(_ => _.Id);
            entity.Property(_ => _.Id).ValueGeneratedOnAdd();
            entity.Property(_ => _.Name).IsRequired().HasMaxLength(250);
            entity.Property(_ => _.UnitCount).IsRequired().ValueGeneratedNever();
            entity.Property(_ => _.ComplexId).IsRequired();

            entity.HasOne(_ => _.Complex)
                .WithMany(_ => _.Blocks)
                .HasForeignKey(_ => _.ComplexId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
