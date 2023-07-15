

namespace SecondComplexManagement.Services.Blocks.Contracts.Dto
{
    public class GetAllBlocksDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UnitCount { get; set; }
        public int AddedUnitCount { get; set; }
        public int RemainedUnitCount { get; set; }
    }
}
