

using SecondComplexManagement.Services.UsageTypes.Contracts.Dto;

namespace SecondComplexManagement.Services.UsageTypes.Contracts
{
    public interface UsageTypeRepository
    {
        public void Add(AddUsageTypeDto dto);
        bool IsDuplicateName(string name);
    }
}
