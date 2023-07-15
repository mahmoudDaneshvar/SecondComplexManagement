using Microsoft.EntityFrameworkCore;
using SecondComplexManagement.Entities;
using SecondComplexManagement.Services.UsageTypes.Contracts;
using SecondComplexManagement.Services.UsageTypes.Contracts.Dto;

namespace SecondComplexManagement.PersistanceEF.UsageTypes
{
    public class EFUsageTypeRepository : UsageTypeRepository
    {
        private readonly DbSet<UsageType> _usageTypes;

        public EFUsageTypeRepository(EFDataContext context)
        {
            _usageTypes = context.Set<UsageType>();
        }

        public void Add(AddUsageTypeDto dto)
        {

            var usageType = new UsageType
            {
                Name = dto.Name,
            };

            _usageTypes.Add(usageType);
        }

        public bool IsDuplicateName(string name)
        {
            return _usageTypes
                .Any(_ => _.Name == name);
        }
    }
}
