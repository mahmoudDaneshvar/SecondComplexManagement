
using System.ComponentModel.DataAnnotations;

namespace SecondComplexManagement.Services.Blocks.Contracts.Dto
{
    public class AddBlockDto
    {
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [Required]
        public int ComplexId { get; set; }

        [Required]
        public int UnitCount { get; set; }
    }
}
