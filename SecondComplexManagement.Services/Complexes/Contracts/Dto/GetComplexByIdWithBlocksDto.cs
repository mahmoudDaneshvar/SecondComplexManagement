

namespace SecondComplexManagement.Services.Complexes.Contracts.Dto
{
    public class GetComplexByIdWithBlocksDto
    {
        public string Name { get; set; }
        public List<BlockDto> Blocks { get; set; }
    }

    public class BlockDto
    {
        public string Name { get; set; }
        public int BlockUntsCount { get; set; }
    }
}
