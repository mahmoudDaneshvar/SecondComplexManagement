using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondComplexManagement.Entities
{
    public class UsageType
    {
        public UsageType()
        {
            BlockUsageTypes = new HashSet<BlockUsageType>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public HashSet<BlockUsageType> BlockUsageTypes { get; set; }
    }
}
