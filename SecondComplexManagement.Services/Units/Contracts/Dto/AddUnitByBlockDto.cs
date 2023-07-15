using SecondComplexManagement.Entities;

namespace SecondComplexManagement.Services.Units.Contracts.Dto
{
    public class AddUnitByBlockDto
    {
        public string Name { get; set; }
        public ResidenceType ResidenceType { get; set; }
        public Block Block { get; set; }
    }
}
