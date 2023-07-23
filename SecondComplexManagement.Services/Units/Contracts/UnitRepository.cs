using SecondComplexManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondComplexManagement.Services.Units.Contracts
{
    public interface UnitRepository
    {
        public void Add(SecondComplexManagement.Entities.Unit unit);
        public void AddRange(List<Unit> units);
        public bool IsDuplicateName(int blockId, string name);
        bool IsExistUnitByComplexId(int id);
    }
}
