using SecondComplexManagement.Entities;
using System.ComponentModel.DataAnnotations;

namespace SecondComplexManagement.Services.Blocks.Contracts.Dto
{
    public class AddBlockWithUnitsDto
    {
        [Required]
        public int ComplexId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int UnitCount { get; set; }

        public HashSet<UnitDto> Units { get; set; }
    }

    public class UnitDto
    {
        [Required]
        public ResidenceType ResidenceType { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
