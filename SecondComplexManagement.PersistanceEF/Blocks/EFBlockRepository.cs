using Microsoft.EntityFrameworkCore;
using SecondComplexManagement.Entities;
using SecondComplexManagement.Services.Blocks.Contracts;

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

        public int GetBlocksUnitsCountByComplexId(int complexId)
        {
            return _blocks
                .Where(_ => _.ComplexId == complexId)
                .Select(_ => _.Units).Count();
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

        public bool IsExistById(int id)
        {
            if (_blocks
                .Any(_ => _.Id == id))
            {
                return true;
            }
            return false;
        }

        
    }
}
