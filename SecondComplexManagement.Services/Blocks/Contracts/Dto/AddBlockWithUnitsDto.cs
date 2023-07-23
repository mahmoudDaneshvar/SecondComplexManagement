using SecondComplexManagement.Services.Units.Contracts.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondComplexManagement.Services.Blocks.Contracts.Dto
{
    public class AddBlockWithUnitsDto
    {
        public AddBlockDto Block { get; set; }

        public List<AddUnitsForBlockDto> Units { get; set; }
    }
}
