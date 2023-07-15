

using SecondComplexManagement.Services.Blocks.Contracts.Dto;

namespace SecondComplexManagement.Services.Blocks.Contracts
{
    public interface BlockService
    {
        public void Add(AddBlockDto dto);
        public void Update(int id, UpdateBlockDto dto);
        public List<GetAllBlocksDto> GetAll();

        public void AddWithUnits(AddBlockWithUnitsDto dto);
        
    }
}
