using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondComplexManagement.Services.Blocks.Contracts.Dto
{
    public class EditBlockDto
    {
        [Required]
        public string Name { get; set; }

        [Range(4,1000)]
        [Required]
        public int UnitCount { get; set; }
    }
}
