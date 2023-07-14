
namespace SecondComplexManagement.Entities
{
    public class Unit
    {
        public int Id { get; set; }
        public ResidenceType ResidenceType { get; set; }
        public string Name { get; set; }
        public int BlockId { get; set; }
        public Block Block { get; set; }
    }
    public enum ResidenceType
    {
        Owner = 1,
        Tenant = 2,
        Empty = 3
    }
}
