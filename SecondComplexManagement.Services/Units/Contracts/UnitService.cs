using SecondComplexManagement.Services.Units.Contracts.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondComplexManagement.Services.Units.Contracts
{
    public interface UnitService
    {
        public void Add(AddUnitDto dto);
    }
}
