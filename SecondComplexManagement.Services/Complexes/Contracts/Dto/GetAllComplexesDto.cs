

namespace SecondComplexManagement.Services.Complexes.Contracts.Dto
{
    public class GetAllComplexesDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AddedUnitsCount { get; set; }
        public int RemainedUnitsCount { get; set; }
    }
}
