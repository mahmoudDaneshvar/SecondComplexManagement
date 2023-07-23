using SecondComplexManagement.Entities;
using SecondComplexManagement.Services.Blocks.Contracts.Dto;
using SecondComplexManagement.Services.Units.Contracts.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondComplexManagement.Services.Blocks.Contracts
{
    public interface BlockService
    {
        public void Add(AddBlockDto dto);
        public void Update(int id,EditBlockDto dto);
        public List<GetAllBlocksDto> GetAll();
        public GetBlockByIdDto? GetById(int id);
        public void AddWithUnits(AddBlockWithUnitsDto dto);
        public void Delete(int id);
    }
}
