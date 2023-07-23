

using SecondComplexManagement.Entities;

namespace SecondComplexManagement.Services.Blocks.Contracts.Dto
{
    public class GetBlockByIdDto
    {
        public string Name { get; set; }
        public List<UnitDto> Units { get; set; }
    }

    public class UnitDto
    {
        public string Name { get; set; }
        public ResidenceType Type { get; set; }
    }
}
