using Microsoft.EntityFrameworkCore;
using SecondComplexManagement.Entities;
using SecondComplexManagement.Services.Blocks.Contracts;
using SecondComplexManagement.Services.Blocks.Contracts.Dto;
using SecondComplexManagement.Services.Blocks.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondComplexManagement.PersistanceEF.Blocks
{
    public class EFBlockRepository : BlockRepository
    {
        private readonly DbSet<Block> _blocks;
        private readonly DbSet<Unit> _units;
        public EFBlockRepository(EFDataContext context)
        {
            _blocks = context.Set<Block>();
            _units = context.Set<Unit>();
        }
        public void Add(Block block)
        {
            _blocks.Add(block);
        }

        public void Delete(Block block)
        {
            _blocks
                .Remove(block);
        }

        public bool DoesHaveUnit(int id)
        {
            return _units
                .Where(_ => _.BlockId == id)
                .Any();
        }

        public Block FindById(int id)
        {
            return _blocks
                .First(_ => _.Id == id);
        }

        public List<GetAllBlocksDto> GetAll()
        {
            return _blocks
                .Select(_ => new GetAllBlocksDto
                {
                    Name = _.Name,
                    UnitCount = _.UnitCount,
                    AddedUnitsCount = _.Units.Count,
                    RemainedUnitsCount = _.UnitCount - _.Units.Count
                }).ToList();
        }

        public GetBlockByIdDto? GetById(int id)
        {
            return _blocks
                .Where(_ => _.Id == id)
                .Select(_ => new GetBlockByIdDto
                {
                    Name = _.Name,
                    Units = _.Units.Select(u => new UnitDto
                    {
                        Name = u.Name,
                        Type = u.ResidenceType
                    }).ToList()
                }).FirstOrDefault();
        }

        public int GetComplexIdById(int id)
        {
            return _blocks
                .Where(_ => _.Id == id)
                .Select(_ => _.ComplexId)
                .First();
        }

        public int GetIdByNameAndComplexId(
            int complexId,
            string name)
        {
            return _blocks
                .Where(_ => _.ComplexId == complexId
                && _.Name == name).Select(_ => _.Id).First();
        }

        public int GetUnitsCountByComplexId(int complexId)
        {
            return _blocks
                .Where(_ => _.ComplexId == complexId)
                .Select(_ => _.UnitCount).Sum();
        }

        public bool IsExistBlockNameByComplexId(int complexId, string name)
        {
            return _blocks
                .Where(_ => _.ComplexId == complexId)
                .Any(_ => _.Name == name);
        }

        public bool IsExistById(int id)
        {
            return _blocks
                .Any(_ => _.Id == id);
        }

        public bool IsFullById(int id)
        {
            if (_blocks.Where(_ => _.Id == id)
                .Any(_ => _.UnitCount == _.Units.Count))
            {
                return true;
            }

            return false;
        }

        public void Update(Block block)
        {
            _blocks.Update(block);
        }
    }
}
