using SecondComplexManagement.Entities;
using SecondComplexManagement.Services.Blocks.Contracts.Dto;

namespace SecondComplexManagement.Services.Blocks.Contracts
{
    public interface BlockRepository
    {
        public void Add(Block block);
        public bool IsExistBlockNameByComplexId(int complexId, string name);
        public bool IsExistById(int id);

        public int GetUnitsCountByComplexId(int complexId);
        public int GetComplexIdById(int id);
        bool IsFullById(int id);

        public Block FindById(int id);
        void Update(Block block);
        public bool DoesHaveUnit(int id);
        public int GetIdByNameAndComplexId(int complexId, string name);
        public List<GetAllBlocksDto> GetAll();
        GetBlockByIdDto? GetById(int id);
        public void Delete(Block block);
    }
}
