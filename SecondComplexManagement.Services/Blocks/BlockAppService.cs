

using SecondComplexManagement.Entities;
using SecondComplexManagement.Services.Blocks.Contracts;
using SecondComplexManagement.Services.Blocks.Contracts.Dto;
using SecondComplexManagement.Services.Blocks.Exceptions;
using SecondComplexManagement.Services.Complexes.Contracts;
using SecondComplexManagement.Services.Complexes.Exceptions;
using SecondComplexManagement.Services.Contracts;

namespace SecondComplexManagement.Services.Blocks
{
    public class BlockAppService : BlockService
    {
        private readonly ComplexRepository _complexRepository;
        private readonly BlockRepository _repository;
        private readonly UnitOfWork _unitOfWork;

        public BlockAppService(
            ComplexRepository complexRepository,
            BlockRepository repository,
            UnitOfWork unitOfWork)
        {
            _complexRepository = complexRepository;
            _repository = repository;
            _unitOfWork = unitOfWork;
        }
        public void Add(AddBlockDto dto)
        {
            var isExistComplex = _complexRepository
                .IsExistById(dto.ComplexId);
            if (!isExistComplex)
            {
                throw new ComplexNotFoundException();
            }

            var isDuplicatedNameInComplex = _repository
                .IsDuplicateNameByComplexId(dto.ComplexId, dto.Name);

            if (isDuplicatedNameInComplex)
            {
                throw new DuplicateBlockNameInSameComplexException();
            }

            var complexUnitCount = _complexRepository
                .GetUnitCountById(dto.ComplexId);

            var complexBlocksUnitCounts = _repository
                .GetBlocksUnitsCountByComplexId(dto.ComplexId);

            if (dto.UnitCount + complexBlocksUnitCounts
                > complexUnitCount)
            {
                throw new UnitCountOutOfComplexRangeException();
            }

            var block = new Block
            {
                ComplexId = dto.ComplexId,
                Name = dto.Name,
                UnitCount = dto.UnitCount,
            };

            _repository.Add(block);
            _unitOfWork.Complete();


        }
    }
}
