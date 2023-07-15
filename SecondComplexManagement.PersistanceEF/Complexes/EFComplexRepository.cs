using Microsoft.EntityFrameworkCore;
using SecondComplexManagement.Entities;
using SecondComplexManagement.Services.Complexes.Contracts;
using SecondComplexManagement.Services.Complexes.Contracts.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondComplexManagement.PersistanceEF.Complexes
{

    public class EFComplexRepository : ComplexRepository
    {
        private readonly EFDataContext _context;
        private readonly DbSet<Complex> _complexes;

        public EFComplexRepository(EFDataContext context)
        {
            _context = context;
            _complexes = context.Set<Complex>();
        }
        public void Add(Complex complex)
        {
            _complexes.Add(complex);
        }

        public List<GetAllComplexesDto> GetAll(
            string? name, int? id)
        {
            var result = _complexes
                .Select(_ => new GetAllComplexesDto
                {
                    Id = _.Id,
                    Name = _.Name,
                    AddedUnitsCount = _.Blocks
                    .SelectMany(_ => _.Units).Count(),
                    RemainedUnitsCount = _.UnitCount - _.Blocks
                    .SelectMany(_ => _.Units).Count()
                });

            if (!string.IsNullOrEmpty(name))
            {
                result = result
                    .Where(_ => _.Name.Contains(name));
            }

            if (id != 0
                && id != null)
            {
                result = result
                    .Where(_ => _.Id == id);
            }

            return result.ToList();
        }

        public GetComplexByIdDto GetById(int id)
        {
            var result = _complexes
                .Where(_ => _.Id == id)
                .Select(_ => new GetComplexByIdDto
                {
                    Id = _.Id,
                    Name = _.Name,
                    AddedUnitsCount = _.Blocks
                    .SelectMany(_ => _.Units).Count(),
                    RemainedUnitsCount = _.UnitCount - _.Blocks
                    .SelectMany(_ => _.Units).Count(),
                    AddedBlocksCount = _.Blocks.Count
                });

            return result.First();
        }

        public GetComplexByIdWithBlocksDto?
            GetByIdWithBlocks(int id, string? blockName)
        {
            var result = _complexes
                .Where(_ => _.Id == id)
                .Select(_ => new GetComplexByIdWithBlocksDto
                {
                    Name = _.Name,
                    Blocks = _.Blocks
                    .Where(_=> blockName != null? _.Name.Contains(blockName):true)
                    .Select(b => new BlockDto
                    {
                        BlockUntsCount = b.Units.Count,
                        Name = b.Name
                    }).ToList()
                });



            return result.FirstOrDefault();
        }



        public int GetUnitCountById(int id)
        {
            return _complexes
                .Where(_ => _.Id == id)
                .Select(_ => _.UnitCount).First();
        }

        public bool IsExistById(int id)
        {
            return _complexes
                .Any(_ => _.Id == id);
        }
    }
}
