using SecondComplexManagement.Services.Complexes.Contracts.Dto;

namespace SecondComplexManagement.Services.Complexes.Contracts
{
    public interface ComplexService
    {
        public void Add(AddComplexDto dto);
        public void EditUnitCount(EditComplexUnitCountDto dto);
        public List<GetAllComplexesDto> GetAll(
            string? name, int? id);

        public GetComplexByIdDto GetById(int id);
        public GetComplexByIdWithBlocksDto? GetByIdWithBlocks(
            int id, string? blockNameB);
    }
}
