

using SecondComplexManagement.Entities;

namespace SecondComplexManagement.Services.Units.Contracts
{
    public interface UnitRepository
    {
        public void Add(Unit unit);
        public bool IsDuplicateUnitNameInBlock(
            int blockId,string name);
    }
}
