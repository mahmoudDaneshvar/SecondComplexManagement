using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondComplexManagement.Entities
{
    public class Block
    {
        public Block()
        {
            Units = new HashSet<Unit> { };
            BlockUsageTypes = new HashSet<BlockUsageType> { };
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public int ComplexId { get; set; }
        public int UnitCount { get; set; }
        public Complex Complex { get; set; }
        public HashSet<Unit> Units { get; set; }
        public HashSet<BlockUsageType> BlockUsageTypes { get; set; }
    }
}
