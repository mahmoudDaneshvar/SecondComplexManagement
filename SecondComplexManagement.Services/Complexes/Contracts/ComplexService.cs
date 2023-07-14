using SecondComplexManagement.Services.Complexes.Contracts.Dto;

namespace SecondComplexManagement.Services.Complexes.Contracts
{
    public interface ComplexService
    {
        public void Add(AddComplexDto dto);
        public void EditUnitCount(EditComplexUnitCountDto dto);
    }
}
