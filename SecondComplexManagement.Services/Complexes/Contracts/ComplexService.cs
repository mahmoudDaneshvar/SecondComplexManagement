using SecondComplexManagement.Services.Complexes.Contracts.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondComplexManagement.Services.Complexes.Contracts
{
    public interface ComplexService
    {
        public void Add(AddComplexDto dto);
        List<GetAllComplexesDto> GetAll(string? searchName);
        public void UpdateUnitCount(int id, int unitCount);
        public GetComplexByIdDto? GetById(int id);
        public GetComplexByIdWithBlocksDto? GetByIdWithBlocks
            (int id,string? blockNameSearch);

        public void Delete(int id);
    }
}
