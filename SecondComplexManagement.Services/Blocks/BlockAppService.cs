

using SecondComplexManagement.Entities;
using SecondComplexManagement.Services.Blocks.Contracts;
using SecondComplexManagement.Services.Blocks.Contracts.Dto;
using SecondComplexManagement.Services.Blocks.Exceptions;
using SecondComplexManagement.Services.Complexes.Contracts;
using SecondComplexManagement.Services.Complexes.Exceptions;
using SecondComplexManagement.Services.Contracts;
using SecondComplexManagement.Services.Units.Contracts;
using SecondComplexManagement.Services.Units.Contracts.Dto;
using SecondComplexManagement.Services.Units.Exceptions;

namespace SecondComplexManagement.Services.Blocks
{
    public class BlockAppService : BlockService
    {
        private readonly ComplexRepository _complexRepository;
        private readonly BlockRepository _repository;
        private readonly UnitOfWork _unitOfWork;
        private readonly UnitRepository _unitRepository;

        public BlockAppService(
            ComplexRepository complexRepository,
            BlockRepository repository,
            UnitOfWork unitOfWork,
            UnitRepository unitRepository)
        {
            _complexRepository = complexRepository;
            _repository = repository;
            _unitOfWork = unitOfWork;
            _unitRepository = unitRepository;
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

        public void AddWithUnits(AddBlockWithUnitsDto dto)
        {
            var isExistComplex = _complexRepository
                .IsExistById(dto.ComplexId);

            if (!isExistComplex)
            {
                throw new ComplexNotFoundException();
            }

            var isDuplicateBlockName =
                _repository
                .IsDuplicateNameByComplexId(
                    dto.ComplexId,
                    dto.Name);

            if (isDuplicateBlockName)
            {
                throw new DuplicateBlockNameInSameComplexException();
            }

            var names = dto.Units.Select(_ => _.Name);

            if (names.Count() != names.Distinct().Count())
            {
                throw new DuplicateUnitNameInSameBlockException();
            }

            var complexUnitCount = _complexRepository
                .GetUnitCountById(dto.ComplexId);

            var complexAddedUnits = _repository
                .GetBlocksUnitsCountByComplexId(dto.ComplexId);

            if (dto.UnitCount + complexAddedUnits
                > complexUnitCount)
            {
                throw new UnitCountOutOfComplexRangeException();
            }

            if (dto.Units.Count() > dto.UnitCount)
            {
                throw new BlockAddedUnitsOutOfRangeException();
            }

            if (dto.Units.Count() + complexAddedUnits
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

            _repository
                .Add(block);

            var units = new List<AddUnitByBlockDto>();

            foreach (var unit in dto.Units)
            {
                units.Add(new AddUnitByBlockDto
                {
                    Name = unit.Name,
                    ResidenceType = unit.ResidenceType,
                    Block = block
                });
            }
            _unitRepository
                .AddRange(units);

            _unitOfWork.Complete();

        }

        public List<GetAllBlocksDto> GetAll()
        {
            return _repository
                .GetAll();
        }

        public void Update(int id, UpdateBlockDto dto)
        {
            var block = _repository
                .FindById(id);

            if (block == null)
            {
                throw new BlockNotFoundException();
            }



            var isDuplicateName = _repository
                 .IsDuplicateNameByComplexId(
                id, dto.Name, block.ComplexId);

            if (isDuplicateName)
            {
                throw new DuplicateBlockNameInSameComplexException();
            }

            var DoesBlockHaveAnyUnit = _repository
                .DoesBlockHaveAnyUnit(id);

            if (!DoesBlockHaveAnyUnit)
            {
                block.UnitCount = dto.UnitCount;
            }

            var complexUnitCount = _complexRepository
                .GetUnitCountById(block.ComplexId);

            var ComplexBlocksUnitCountsExceptThisBlock
                = _repository.ComplexBlocksUnitCountsExceptThisBlock(
                    id, block.ComplexId);

            if (
                ComplexBlocksUnitCountsExceptThisBlock + dto.UnitCount
                > complexUnitCount)
            {
                throw new UnitCountOutOfComplexRangeException();
            }

            block.Name = dto.Name;

            _repository.Update(block);
            _unitOfWork.Complete();
        }
    }
}
