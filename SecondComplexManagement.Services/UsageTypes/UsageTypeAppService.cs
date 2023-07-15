using SecondComplexManagement.Services.Contracts;
using SecondComplexManagement.Services.UsageTypes.Contracts;
using SecondComplexManagement.Services.UsageTypes.Contracts.Dto;
using SecondComplexManagement.Services.UsageTypes.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SecondComplexManagement.Services.UsageTypes
{

    
    public class UsageTypeAppService : UsageTypeService
    {
        private readonly UsageTypeRepository _repository;
        private readonly UnitOfWork _unitOfWork;

        public UsageTypeAppService(
            UsageTypeRepository repository,
            UnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }
        public void Add(AddUsageTypeDto dto)
        {
            if (_repository.IsDuplicateName(dto.Name))
            {
                throw new DuplicateUsageTypeNameException();
            }
            _repository.Add(dto);
            _unitOfWork.Complete();
        }

        
    }
}
