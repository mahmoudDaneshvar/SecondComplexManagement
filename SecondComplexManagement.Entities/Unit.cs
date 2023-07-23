using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondComplexManagement.Entities
{
    public class Unit
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ResidenceType ResidenceType { get; set; }
        public int BlockId { get; set; }
        public Block Block { get; set; }

    }

    public enum ResidenceType
    {
        Owner,
        Tenant,
        Anonymous
    }
}
