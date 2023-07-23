using SecondComplexManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondComplexManagement.Services.Unit.Test.Factories
{
    public static class UnitFactory
    {
        public static Entities.Unit CreateUnit(
            Block block,
            string name = "dummy",
            ResidenceType type = ResidenceType.Owner)
        {
            return new Entities.Unit
            {
                Block = block,
                Name = name,
                ResidenceType = type
            };
        }
    }
}
