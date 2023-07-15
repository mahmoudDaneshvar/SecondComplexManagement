using Microsoft.EntityFrameworkCore;
using SecondComplexManagement.Entities;
using SecondComplexManagement.Services.Units.Contracts;
using SecondComplexManagement.Services.Units.Contracts.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondComplexManagement.PersistanceEF.Units
{
    public class EFUnitRepository : UnitRepository
    {
        private readonly DbSet<Unit> _units;

        public EFUnitRepository(EFDataContext context)
        {
            _units = context.Set<Unit>();
        }

        public void Add(Unit unit)
        {
            _units.Add(unit);
        }

        public void AddRange(List<AddUnitByBlockDto> unitsDto)
        {
            var units = new List<Unit>();

            foreach (var unitDto in unitsDto)
            {
                units.Add(new Unit
                {
                    Block = unitDto.Block,
                    Name = unitDto.Name,
                    ResidenceType = unitDto.ResidenceType
                });
            }

            _units.AddRange(units);
        }

        public bool IsDuplicateUnitNameInBlock(
            int blockId, string name)
        {
            if (_units
                .Where(_ => _.BlockId == blockId)
                .Any(_ => _.Name == name))
            {
                return true;
            }

            return false;
        }
    }
}
