

using SecondComplexManagement.Entities;

namespace SecondComplexManagement.Services.Units.Contracts.Dto
{
    public class AddUnitDto
    {
        public ResidenceType ResidenceType { get; set; }
        public string Name { get; set; }
        public int BlockId { get; set; }
    }
}
