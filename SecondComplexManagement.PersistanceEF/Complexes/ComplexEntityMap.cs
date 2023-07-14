using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SecondComplexManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondComplexManagement.PersistanceEF.Complexes
{
    internal class ComplexEntityMap : IEntityTypeConfiguration<Complex>
    {
        public void Configure(EntityTypeBuilder<Complex> entity)
        {
            entity.ToTable("Complexes");
            entity.HasKey(_ => _.Id);
            entity.Property(_ => _.Id).ValueGeneratedOnAdd();
            entity.Property(_ => _.Name).IsRequired().HasMaxLength(225);
            entity.Property(_ => _.UnitCount).IsRequired().ValueGeneratedNever();

        }
    }
}
