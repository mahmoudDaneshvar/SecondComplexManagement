using Microsoft.EntityFrameworkCore;
using SecondComplexManagement.Entities;
using SecondComplexManagement.Services.Complexes.Contracts;
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
