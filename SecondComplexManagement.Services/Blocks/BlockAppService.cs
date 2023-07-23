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
        private readonly BlockRepository _repository;
        private readonly UnitOfWork _unitOfWork;
        private readonly ComplexRepository _complexRepository;
        private readonly UnitRepository _unitRepository;

        public BlockAppService(
            BlockRepository repository,
            UnitOfWork unitOfWork,
            ComplexRepository complexRepository,
            UnitRepository unitRepository)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _complexRepository = complexRepository;
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
            if (_repository
                .IsExistBlockNameByComplexId(dto.ComplexId, dto.Name))
            {
                throw new DuplicateBlockNameInSameComplexException();
            }
            if (dto.UnitCount >
                _complexRepository.GetUnitCountById(dto.ComplexId))
            {
                throw new BlockUnitCountOutOfRangeException();
            }

            var complexUnitCount = _complexRepository
                .GetUnitCountById(dto.ComplexId);

            var blockUnitsCountOfComplex = _repository
                .GetUnitsCountByComplexId(dto.ComplexId);

            if (blockUnitsCountOfComplex + dto.UnitCount
                > complexUnitCount)
            {
                throw new ComplexIsFullException();
            }



            var block = new Block
            {
                ComplexId = dto.ComplexId,
                Name = dto.Name,
                UnitCount = dto.UnitCount
            };

            _repository.Add(block);
            _unitOfWork.Complete();
        }

        public void AddWithUnits(AddBlockWithUnitsDto dto)
        {
            var isDuplicateBlockName =
                _repository.IsExistBlockNameByComplexId(
                    dto.Block.ComplexId, dto.Block.Name);

            if (isDuplicateBlockName)
            {
                throw new DuplicateBlockNameInSameComplexException();
            }
            var complexBlockUnitsCount = _repository
                .GetUnitsCountByComplexId(dto.Block.ComplexId);

            var complexUnitCount = _complexRepository
                .GetUnitCountById(dto.Block.ComplexId);

            if (complexBlockUnitsCount + dto.Block.UnitCount
                > complexUnitCount)
            {
                throw new ComplexIsFullException();
            }
            

            var isDuplicateUnitName = dto.Units
                .Select(_ => _.Name).Distinct().Count()
                == dto.Units.Select(_ => _.Name).Count() ? false : true;

            if (isDuplicateUnitName)
            {
                throw new DuplicateUnitNameInSameBlockException();
            }

            var isBlockFull = dto.Units.Count > dto.Block.UnitCount ? 
                true : false;

            if(isBlockFull)
            {
                throw new BlockIsFullException();
            }


            var block = new Block
            {
                ComplexId = dto.Block.ComplexId,
                Name = dto.Block.Name,
                UnitCount = dto.Block.UnitCount,
            };

            var units = new List<Unit>();

            foreach (var unit in dto.Units)
            {
                units.Add(new Unit
                {
                    Block = block,
                    Name = unit.Name,
                    ResidenceType = unit.Type
                });
            }

            _unitRepository.AddRange(units);
            _unitOfWork.Complete();

        }

        public void Delete(int id)
        {
            var isExistBlock = _repository
                .IsExistById(id);
            if (!isExistBlock)
            {
                throw new BlockNotFoundException();
            }

            var doesBlockHaveUnit = _repository
                .DoesHaveUnit(id);

            if (doesBlockHaveUnit)
            {
                throw new BlockHasUnitException();
            }

            var block = _repository
                .FindById(id);
            _repository.Delete(block);
            _unitOfWork.Complete();
        }

        public List<GetAllBlocksDto> GetAll()
        {
            return _repository
                .GetAll();
        }

        public GetBlockByIdDto? GetById(int id)
        {
            return _repository
                .GetById(id);
        }

        public void Update(int id, EditBlockDto dto)
        {
            var isExistsBlock = _repository
                .IsExistById(id);

            if (!isExistsBlock)
            {
                throw new BlockNotFoundException();
            }

            var complexId = _repository
                .GetComplexIdById(id);

            var isDuplicateBlockName = _repository
                .IsExistBlockNameByComplexId(complexId, dto.Name);

            if (isDuplicateBlockName)
            {
                var IdWhereDuplicateName
                     = _repository
                     .GetIdByNameAndComplexId(complexId, dto.Name);

                if (id != IdWhereDuplicateName)
                {
                    throw new DuplicateBlockNameInSameComplexException();
                }

            }


            var block = _repository
                .FindById(id);

            block.Name = dto.Name;

            var doesBlockHaveAnyUnit = _repository
                .DoesHaveUnit(id);

            if (!doesBlockHaveAnyUnit)
            {
                block.UnitCount = dto.UnitCount;

            }


            _repository.Update(block);
            _unitOfWork.Complete();
        }
    }
}
