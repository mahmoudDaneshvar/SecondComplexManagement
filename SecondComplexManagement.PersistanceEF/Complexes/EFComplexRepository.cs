using Microsoft.EntityFrameworkCore;
using SecondComplexManagement.Entities;
using SecondComplexManagement.PersistanceEF;
using SecondComplexManagement.Services.Complexes.Contracts;
using SecondComplexManagement.Services.Complexes.Contracts.Dto;

namespace SecondComplexManagement.Services.Unit.Test.Complexes
{

    public class EFComplexRepository : ComplexRepository
    {
        private DbSet<Complex> _complexes;
        private DbSet<Block> _blocks;

        public EFComplexRepository(EFDataContext context)
        {
            _complexes = context.Set<Complex>();
            _blocks = context.Set<Block>();
        }
        public void Add(Complex complex)
        {
            _complexes.Add(complex);
        }

        public Complex? FindById(int id)
        {
            return _complexes
                .SingleOrDefault(_ => _.Id == id);
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

        public int GetCountOfUnitsById(int id)
        {
            return _blocks
                .Where(_ => _.ComplexId == id)
                .SelectMany(_ => _.Units).Count();

        }

        public void Update(Complex complex)
        {
            _complexes
                .Update(complex);
        }

        public List<GetAllComplexesDto> GetAllWithSearchName(string? searchName)
        {
            var result = _complexes
                .Select(_ => new GetAllComplexesDto
                {
                    Id = _.Id,
                    Name = _.Name,
                    AddedUnitCount = _.Blocks.SelectMany(_ => _.Units).Count(),
                    RemainedUnitsCount = _.UnitCount - _.Blocks.SelectMany(_ => _.Units).Count()
                });

            if (!string.IsNullOrEmpty(searchName))
            {
                result = result
                    .Where(_ => _.Name.Contains(searchName));
            }

            return result.ToList();
        }

        public GetComplexByIdDto? GetById(int id)
        {
            return _complexes
                .Where(_ => _.Id == id)
                .Select(_ => new GetComplexByIdDto
                {
                    Id = _.Id,
                    Name = _.Name,

                    AddedUnitsCount = _.Blocks
                    .SelectMany(_ => _.Units).Count(),

                    RemainedUnitsCount = _.UnitCount - _.Blocks
                    .SelectMany(_ => _.Units).Count(),

                    AddedBlocksCount = _.Blocks.Count()
                }).FirstOrDefault();
        }

        public GetComplexByIdWithBlocksDto? GetByIdWithBlocks(
            int id,
            string? blockNameSearch)
        {
            return _complexes
                .Where(_ => _.Id == id)
                .Select(_ => new GetComplexByIdWithBlocksDto
                {
                    Id = _.Id,
                    Name = _.Name,
                    Blocks = _.Blocks
                    .Where(b => blockNameSearch != null ? b.Name
                    .Contains(blockNameSearch) : true)
                    .Select(b => new BlockDto
                    {
                        Name = b.Name,
                        AddedUnitsCount = b.Units.Count()
                    }).ToList()
                }).FirstOrDefault();
        }

        public void Delete(Complex complex)
        {
            _complexes.Remove(complex);
        }
    }
}
