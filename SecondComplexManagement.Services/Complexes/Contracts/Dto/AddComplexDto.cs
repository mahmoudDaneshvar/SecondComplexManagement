

using System.ComponentModel.DataAnnotations;

namespace SecondComplexManagement.Services.Complexes.Contracts.Dto
{
    public class AddComplexDto
    {
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [Required]
        [Range(4, 1000)]
        public int UnitCount { get; set; }
    }
}
