using SecondComplexManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondComplexManagement.Services.Unit.Test.Factories
{
    public static class BlockFactory
    {
        public static Block CreateBlock(
            Complex complex,
            string? name = null,
            int unitCount = 0
            )
        {

            if (name == null)
            {
                name = "dummy";
            }

            if(unitCount == 0)
            {
                unitCount = 5;
            }

            return new Block()
            {
                Complex = complex,
                Name = name,
                UnitCount = unitCount
            };
        }
    }
}
