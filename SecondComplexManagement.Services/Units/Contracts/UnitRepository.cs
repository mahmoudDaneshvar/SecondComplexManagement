

using SecondComplexManagement.Entities;
using SecondComplexManagement.Services.Units.Contracts.Dto;

namespace SecondComplexManagement.Services.Units.Contracts
{
    public interface UnitRepository
    {
        public void Add(Unit unit);
        public bool IsDuplicateUnitNameInBlock(
            int blockId,string name);

        public void AddRange(List<AddUnitByBlockDto> units);
    }
}
