

namespace SecondComplexManagement.Entities
{
    public class BlockUsageType
    {
        public int BlockId { get; set; }
        public int UsageTypeId { get; set; }

        public Block Block { get; set; }
        public UsageType UsageType { get; set; }
    }
}
