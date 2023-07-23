using SecondComplexManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondComplexManagement.Services.Units.Contracts.Dto
{
    public class AddUnitsForBlockDto
    {
        public string Name { get; set; }
        public ResidenceType Type { get; set; }
    }
}
