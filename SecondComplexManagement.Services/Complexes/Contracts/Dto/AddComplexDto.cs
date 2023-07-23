
using System.ComponentModel.DataAnnotations;

namespace SecondComplexManagement.Services.Complexes.Contracts.Dto
{
    public class AddComplexDto
    {
        [Required]
        public string Name { get; set; }
        [Range(4,1000)]
        public int UnitCount { get; set; }
    }
}
