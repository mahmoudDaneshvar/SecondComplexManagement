using Microsoft.EntityFrameworkCore;
using SecondComplexManagement.Entities;
using SecondComplexManagement.Services.Units.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondComplexManagement.PersistanceEF.Units
{
    public class EFUnitRepository : UnitRepository
    {
        private readonly DbSet<Entities.Unit> _units;

        public EFUnitRepository(EFDataContext context)
        {
            _units = context.Set<Entities.Unit>();
        }
        public void Add(Entities.Unit unit)
        {
            _units.Add(unit);
        }

        public void AddRange(List<Unit> units)
        {
            _units.AddRange(units);
        }

        public bool IsDuplicateName(int blockId, string name)
        {
            return _units
                .Any(_ => _.BlockId == blockId
                && _.Name == name);
        }

        public bool IsExistUnitByComplexId(int id)
        {
            return _units
                .Any(_ => _.Block.ComplexId == id);
        }
    }
}
