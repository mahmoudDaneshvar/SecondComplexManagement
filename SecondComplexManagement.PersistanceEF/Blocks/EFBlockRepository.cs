using Microsoft.EntityFrameworkCore;
using SecondComplexManagement.Entities;
using SecondComplexManagement.Services.Blocks.Contracts;
using SecondComplexManagement.Services.Blocks.Contracts.Dto;

namespace SecondComplexManagement.PersistanceEF.Blocks
{

    public class EFBlockRepository : BlockRepository
    {
        private readonly DbSet<Block> _blocks;
        public EFBlockRepository(EFDataContext context)
        {
            _blocks = context.Set<Block>();
        }

        public void Add(Block block)
        {
            _blocks.Add(block);
        }

        public int ComplexBlocksUnitCountsExceptThisBlock(
            int id, int complexId)
        {
            return _blocks
                .Where(_ => _.ComplexId == complexId
                && _.Id != id)
                .Select(_ => _.Units).Count();
        }

        public bool DoesBlockHaveAnyUnit(int id)
        {

            return _blocks
                .Where(_ => _.Id == id)
                .SelectMany(_ => _.Units).Any();
        }

        public Block? FindById(int id)
        {
            return _blocks
                .FirstOrDefault(_ => _.Id == id);
        }

        public List<GetAllBlocksDto> GetAll()
        {
            return _blocks
                .Select(block => new GetAllBlocksDto
                {
                    Id = block.Id,
                    Name = block.Name,
                    UnitCount = block.UnitCount,
                    AddedUnitCount = block.Units.Count,
                    RemainedUnitCount = block.UnitCount - block.Units.Count
                }).ToList();
        }

        public int GetBlocksUnitsCountByComplexId(int complexId)
        {
            return _blocks
                .Where(_ => _.ComplexId == complexId)
                .Select(_ => _.Units).Count();
        }

        public int GetComplexIdById(int id)
        {
            return
                _blocks.Where(_ => _.Id == id)
                .Select(_ => _.ComplexId)
                .First();
        }

        public bool IsDuplicateNameByComplexId(
            int complexId, string name)
        {
            if (_blocks
                .Where(_ => _.ComplexId == complexId)
                .Any(_ => _.Name == name))
            {
                return true;
            }
            return false;
        }

        public bool IsDuplicateNameByComplexId(
            int id, string name, int complexId)
        {
            return _blocks
                .Where(_ => _.ComplexId == complexId)
                .Any(_ => _.Name == name
                && _.Id != id);
        }

        public bool IsExistById(int id)
        {
            if (_blocks
                .Any(_ => _.Id == id))
            {
                return true;
            }
            return false;
        }

        public bool IsFullById(int id)
        {
            return _blocks
                .Where(_ => _.Id == id)
                .Any(_ => _.Units.Count == _.UnitCount);
        }

        public void Update(Block block)
        {
            _blocks.Update(block);
        }
    }
}
