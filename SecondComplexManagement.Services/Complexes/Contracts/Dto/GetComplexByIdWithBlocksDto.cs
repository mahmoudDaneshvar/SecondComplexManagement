using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondComplexManagement.Services.Complexes.Contracts.Dto
{
    public class GetComplexByIdWithBlocksDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<BlockDto> Blocks { get; set; }
    }

    public class BlockDto
    {
        public string Name { get; set; }
        public int AddedUnitsCount { get; set; }
    }
}
