

using SecondComplexManagement.Entities;

namespace SecondComplexManagement.Services.Blocks.Contracts
{
    public interface BlockRepository
    {
        public bool IsDuplicateNameByComplexId(int complexId, string name);
        public int GetBlocksUnitsCountByComplexId(int complexId);
        public void Add(Block block);
        public bool IsExistById(int id);
    }
}
