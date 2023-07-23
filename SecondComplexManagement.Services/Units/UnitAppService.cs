using SecondComplexManagement.Services.Contracts;
using SecondComplexManagement.Services.Units.Contracts.Dto;
using SecondComplexManagement.Services.Units.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SecondComplexManagement.Services.Blocks.Exceptions;
using SecondComplexManagement.Services.Blocks.Contracts;
using SecondComplexManagement.Services.Units.Exceptions;
using SecondComplexManagement.Services.Complexes.Contracts;
using SecondComplexManagement.Services.Complexes.Exceptions;

namespace SecondComplexManagement.Services.Units
{
    public class UnitAppService : UnitService
    {
        private readonly UnitRepository _repository;
        private readonly UnitOfWork _unitOfWork;
        private readonly BlockRepository _blockRepository;
        private readonly ComplexRepository _complexRepository;


        public UnitAppService(
            UnitRepository repository,
            UnitOfWork unitOfWork,
            BlockRepository blockRepository,
            ComplexRepository complexRepository)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _blockRepository = blockRepository;
            _complexRepository = complexRepository;
        }
        public void Add(AddUnitDto dto)
        {
            if (!_blockRepository
                .IsExistById(dto.BlockId))
            {
                throw new BlockNotFoundException();
            }

            if (_repository.IsDuplicateName(dto.BlockId, dto.Name))
            {
                throw new DuplicateUnitNameInSameBlockException();
            }

            var complexId = _blockRepository
                .GetComplexIdById(dto.BlockId);

            var complexCountOfUnits = _complexRepository
                .GetCountOfUnitsById(complexId);

            var complexUnitCount = _complexRepository
                .GetUnitCountById(complexId);

            if(complexCountOfUnits == complexUnitCount)
            {
                throw new ComplexIsFullException();
            }

            var isBlockFull = _blockRepository
                .IsFullById(dto.BlockId);

            if (isBlockFull)
            {
                throw new BlockIsFullException();
            }

            var unit = new Entities.Unit
            {
                Name = dto.Name,
                BlockId = dto.BlockId,
                ResidenceType = dto.ResidenceType
            };
            _repository.Add(unit);
            _unitOfWork.Complete();
        }
    }
}
