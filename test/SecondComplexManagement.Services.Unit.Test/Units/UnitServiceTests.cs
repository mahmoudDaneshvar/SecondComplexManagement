
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using SecondComplexManagement.Entities;
using SecondComplexManagement.PersistanceEF;
using SecondComplexManagement.PersistanceEF.Blocks;
using SecondComplexManagement.PersistanceEF.Units;
using SecondComplexManagement.Services.Blocks.Exceptions;
using SecondComplexManagement.Services.Complexes.Exceptions;
using SecondComplexManagement.Services.Contracts;
using SecondComplexManagement.Services.Unit.Test.Complexes;
using SecondComplexManagement.Services.Unit.Test.Factories;
using SecondComplexManagement.Services.Units;
using SecondComplexManagement.Services.Units.Contracts;
using SecondComplexManagement.Services.Units.Contracts.Dto;
using SecondComplexManagement.Services.Units.Exceptions;
using TaavArchitecture.TestTools.Infrastructure.DataBaseConfig;
using TaavArchitecture.TestTools.Infrastructure.DataBaseConfig.Unit;

namespace SecondComplexManagement.Services.Unit.Test.Units
{
    public class UnitServiceTests : BusinessUnitTest
    {
        private readonly UnitAppService _sut;

        public UnitServiceTests()
        {
            var unitRepository = new EFUnitRepository(SetupContext);
            var unitOfWork = new EFUnitOfWork(SetupContext);
            var blockRepository = new EFBlockRepository(SetupContext);
            var complexRepository = new EFComplexRepository(SetupContext);
            _sut = new UnitAppService(
                unitRepository,
                unitOfWork,
                blockRepository,
                complexRepository
                );
        }

        [Theory]
        [InlineData(ResidenceType.Owner)]
        [InlineData(ResidenceType.Tenant)]
        [InlineData(ResidenceType.Anonymous)]
        public void Add_add_unit_properly(ResidenceType type)
        {

            var complex = ComplexFactory.CreateComplex();


            var block = BlockFactory.CreateBlock(complex);

            DbContext.Save(block);

            var unit = new AddUnitDto
            {
                Name = "dummy",
                BlockId = block.Id,
                ResidenceType = type
            };

            _sut.Add(unit);

            var expected = ReadContext.Set<Entities.Unit>().Single();

            expected.Name.Should().Be(unit.Name);
            expected.ResidenceType.Should().Be(type);
            expected.BlockId.Should().Be(block.Id);

        }

        [Fact]
        public void Add_throw_exception_when_block_not_found()
        {
            var invalidBlockId = -1;

            var unit = new AddUnitDto
            {
                Name = "dummy",
                BlockId = invalidBlockId,
                ResidenceType = ResidenceType.Owner
            };

            var expected = () => _sut.Add(unit);

            expected.Should().ThrowExactly<BlockNotFoundException>();
        }

        [Fact]
        public void Add_throw_exception_when_duplicate_name()
        {
            var complex = ComplexFactory.CreateComplex();


            var block = BlockFactory.CreateBlock(complex);

            DbContext.Save(block);

            var unit = new AddUnitDto
            {
                Name = "dummy",
                BlockId = block.Id,
                ResidenceType = ResidenceType.Owner
            };
            var unit2 = new AddUnitDto
            {
                Name = "dummy",
                BlockId = block.Id,
                ResidenceType = ResidenceType.Owner
            };

            _sut.Add(unit);
            var expected = () => _sut.Add(unit2);

            expected.Should()
                .Throw<DuplicateUnitNameInSameBlockException>();
        }

        [Fact]
        public void Add_throw_complex_is_full_exception()
        {
            var complex = ComplexFactory
                .CreateComplex("dummy", 1);


            var block = BlockFactory.CreateBlock(complex,unitCount : 1);
            DbContext.Save(block);

            var unit = new AddUnitDto
            {
                Name = "dummy",
                BlockId = block.Id,
                ResidenceType = ResidenceType.Owner
            };
            _sut.Add(unit);

            var unit2 = new AddUnitDto
            {
                Name = "dummy2",
                BlockId = block.Id,
                ResidenceType = ResidenceType.Owner
            };

            var expected = () => _sut.Add(unit2);
            expected.Should().ThrowExactly<ComplexIsFullException>();
        }

        [Fact]
        public void Add_throw_exception_when_block_is_full()
        {
            var complex = ComplexFactory
                .CreateComplex();

            var block = BlockFactory.CreateBlock(complex, unitCount: 1);
            DbContext.Save(block);
            var unit = new AddUnitDto
            {
                Name = "dummy",
                BlockId = block.Id,
                ResidenceType = ResidenceType.Owner,

            };
            _sut.Add(unit);
            var unit2 = new AddUnitDto
            {
                Name = "dummy2",
                BlockId = block.Id,
                ResidenceType = ResidenceType.Owner,

            };
            var expected = () => _sut.Add(unit2);

            expected.Should().Throw<BlockIsFullException>();

        }
    }










}
