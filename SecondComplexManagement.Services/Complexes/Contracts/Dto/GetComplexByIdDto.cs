using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondComplexManagement.Services.Complexes.Contracts.Dto
{
    public class GetComplexByIdDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AddedUnitsCount { get; set; }
        public int RemainedUnitsCount { get; set; }
        public int AddedBlocksCount { get; set; }
    }
}
