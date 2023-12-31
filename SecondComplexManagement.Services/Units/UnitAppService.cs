﻿using SecondComplexManagement.Entities;
using SecondComplexManagement.Services.Blocks.Contracts;
using SecondComplexManagement.Services.Blocks.Exceptions;
using SecondComplexManagement.Services.Contracts;
using SecondComplexManagement.Services.Units.Contracts;
using SecondComplexManagement.Services.Units.Contracts.Dto;
using SecondComplexManagement.Services.Units.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondComplexManagement.Services.Units
{
    public class UnitAppService : UnitService
    {
        private readonly UnitRepository _repository;
        private readonly BlockRepository _blockRepository;
        private readonly UnitOfWork _unitOfWork;
        public UnitAppService(
            UnitRepository unitRepository,
            BlockRepository blockRepository,
            UnitOfWork unitOfWork)
        {
            _repository = unitRepository;
            _blockRepository = blockRepository;
            _unitOfWork = unitOfWork;
        }

        public void Add(AddUnitDto dto)
        {
            var isExistBlock = _blockRepository
                .IsExistById(dto.BlockId);

            if (!isExistBlock)
            {
                throw new BlockNotFoundException();
            }

            var isDuplicateUnitNameInBlock = _repository
                .IsDuplicateUnitNameInBlock(dto.BlockId, dto.Name);

            if (isDuplicateUnitNameInBlock)
            {
                throw new DuplicateUnitNameInSameBlockException();
            }

            var unit = new Unit
            {
                Name = dto.Name,
                BlockId = dto.BlockId
            };
            _repository.Add(unit);
            _unitOfWork.Complete();
            
        }
    }
}
