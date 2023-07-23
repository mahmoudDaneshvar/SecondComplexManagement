using SecondComplexManagement.Entities;
using SecondComplexManagement.Services.Complexes.Contracts.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondComplexManagement.Services.Complexes.Contracts
{
    public interface ComplexRepository
    {
        public void Add(Complex complex);
        public Complex? FindById(int id);
        public int GetUnitCountById(int id);
        public bool IsExistById(int id);
        public int GetCountOfUnitsById(int id);
        void Update(Complex complex);
        List<GetAllComplexesDto> GetAllWithSearchName(
            string? searchName);
        public GetComplexByIdDto? GetById(int id);
        public GetComplexByIdWithBlocksDto? GetByIdWithBlocks(
            int id,
            string? blockNameSearch);
        public void Delete(Complex complex);
    }
}
