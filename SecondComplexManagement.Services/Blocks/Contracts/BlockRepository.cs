

using SecondComplexManagement.Entities;
using SecondComplexManagement.Services.Blocks.Contracts.Dto;

namespace SecondComplexManagement.Services.Blocks.Contracts
{
    public interface BlockRepository
    {
        public bool IsDuplicateNameByComplexId(int complexId, string name);
        public int GetBlocksUnitsCountByComplexId(int complexId);
        public void Add(Block block);
        public bool IsExistById(int id);
        public bool IsFullById(int id);
        public int GetComplexIdById(int id);
        public bool IsDuplicateNameByComplexId(
            int id, string name, int complexId);
        public Block? FindById(int id);
        public bool DoesBlockHaveAnyUnit(int id);

        public void Update(Block block);
        public int ComplexBlocksUnitCountsExceptThisBlock(
            int id, int complexId);

        public List<GetAllBlocksDto> GetAll();
    }
}
