using Microsoft.EntityFrameworkCore;
using SecondComplexManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondComplexManagement.PersistanceEF
{
    public class EFDataContext : DbContext
    {
        public EFDataContext(DbContextOptions<EFDataContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EFDataContext).Assembly);
            modelBuilder.Entity<BlockUsageType>().HasKey(_ => new
            {
                _.BlockId,
                _.UsageTypeId
            });
        }
    }
}
