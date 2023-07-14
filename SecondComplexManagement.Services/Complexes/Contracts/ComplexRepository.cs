

using SecondComplexManagement.Entities;

namespace SecondComplexManagement.Services.Complexes.Contracts
{
    public interface ComplexRepository
    {
        public void Add(Complex complex);
        public bool IsExistById(int id);
        public int GetUnitCountById(int id);
    }
}
