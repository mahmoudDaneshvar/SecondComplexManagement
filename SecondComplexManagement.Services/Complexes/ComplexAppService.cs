using SecondComplexManagement.Entities;
using SecondComplexManagement.Services.Complexes.Contracts;
using SecondComplexManagement.Services.Complexes.Contracts.Dto;
using SecondComplexManagement.Services.Complexes.Exceptions;
using SecondComplexManagement.Services.Contracts;

namespace SecondComplexManagement.Services.Complexes
{
    public class ComplexAppService : ComplexService
    {
        private readonly ComplexRepository _repository;
        private readonly UnitOfWork _unitOfWork;
        public ComplexAppService(
            ComplexRepository complexRepository,
            UnitOfWork unitOfWork)
        {
            _repository = complexRepository;
            _unitOfWork = unitOfWork;
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

        public void EditUnitCount(
            EditComplexUnitCountDto dto)
        {
            var isExistComplex = _repository
                .IsExistById(dto.ComplexId);
        }

        public List<GetAllComplexesDto> GetAll(string? name, int? id)
        {
            return _repository
                .GetAll(name, id);
        }

        public GetComplexByIdDto GetById(int id)
        {
            var isExistComplex = _repository
                .IsExistById(id);

            if (!isExistComplex)
            {
                throw new ComplexNotFoundException();
            }

            return _repository
                .GetById(id);
        }

        public GetComplexByIdWithBlocksDto?
            GetByIdWithBlocks(int id, string? blockName)
        {
            return _repository
                .GetByIdWithBlocks(id, blockName);


        }
    }
}
