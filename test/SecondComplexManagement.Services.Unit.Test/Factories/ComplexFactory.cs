using SecondComplexManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondComplexManagement.Services.Unit.Test.Factories
{
    public static class ComplexFactory
    {
        public static Complex CreateComplex(
            string? name = null,
            int unitCount = 0)
        {
            if(name == null)
            {
                name = "dummy";
            }

            if(unitCount == 0)
            {
                unitCount = 20;
            }

            return new Complex()
            {
                Name = name,
                UnitCount = unitCount
            };
        }
    }
}
