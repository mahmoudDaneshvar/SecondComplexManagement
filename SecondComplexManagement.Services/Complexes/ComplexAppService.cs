using SecondComplexManagement.Services.Complexes.Contracts.Dto;
using SecondComplexManagement.Services.Complexes.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SecondComplexManagement.Services.Contracts;
using SecondComplexManagement.Entities;
using SecondComplexManagement.Services.Units.Contracts;
using SecondComplexManagement.Services.Complexes.Exceptions;

namespace SecondComplexManagement.Services.Complexes
{
    public class ComplexAppService : ComplexService
    {
        private readonly ComplexRepository _repository;
        private readonly UnitOfWork _unitOfWork;
        private readonly UnitRepository _unitRepository;

        public ComplexAppService(
            ComplexRepository repository,
            UnitOfWork unitOfWork,
            UnitRepository unitRepository)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _unitRepository = unitRepository;
        }
        public void Add(AddComplexDto dto)
        {
            var complex = new Complex
            {
                Name = dto.Name,
                UnitCount = dto.UnitCount,
            };

            _repository.Add(complex);
            _unitOfWork.Complete();
        }

        public void Delete(int id)
        {
            var isExistComplex = _repository
                .IsExistById(id);

            if (!isExistComplex)
            {
                throw new ComplexNotFoundException();
            }

            var doesComplexHaveUnit = _unitRepository
                .IsExistUnitByComplexId(id);

            if (doesComplexHaveUnit)
            {
                throw new ComplexHasUnitException();
            }

            var complex = _repository
                .FindById(id);
            _repository
                .Delete(complex);
            _unitOfWork.Complete();

        }

        public List<GetAllComplexesDto> GetAll(string? searchName)
        {
            return _repository
                .GetAllWithSearchName(searchName);
        }

        public GetComplexByIdDto? GetById(int id)
        {
            return _repository
                .GetById(id);
        }

        public GetComplexByIdWithBlocksDto? GetByIdWithBlocks(
            int id,
            string? blockNameSearch)
        {
            return _repository
                .GetByIdWithBlocks(id, blockNameSearch);
        }

        public void UpdateUnitCount(int id, int unitCount)
        {
            var isExistComplex = _repository
                .IsExistById(id);

            if (!isExistComplex)
            {
                throw new ComplexNotFoundException();
            }

            var doesComplexHasUnit = _unitRepository
                .IsExistUnitByComplexId(id);

            if(doesComplexHasUnit)
            {
                throw new ComplexHasUnitException();
            }

            var complex = _repository
                .FindById(id);

            complex.UnitCount = unitCount;

            _repository.Update(complex);
            _unitOfWork.Complete();
        }
    }
}
