

using SecondComplexManagement.Entities;
using SecondComplexManagement.Services.Complexes.Contracts.Dto;

namespace SecondComplexManagement.Services.Complexes.Contracts
{
    public interface ComplexRepository
    {
        public void Add(Complex complex);
        public bool IsExistById(int id);
        public int GetUnitCountById(int id);
        public List<GetAllComplexesDto> GetAll(
            string? name, int? id);
        public GetComplexByIdDto GetById(int id);

        public GetComplexByIdWithBlocksDto ?
            GetByIdWithBlocks(int id, string? blockName);
    }
}
