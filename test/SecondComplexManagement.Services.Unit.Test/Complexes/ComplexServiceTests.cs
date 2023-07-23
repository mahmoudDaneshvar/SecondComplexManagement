
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SecondComplexManagement.Entities;
using SecondComplexManagement.PersistanceEF;
using SecondComplexManagement.PersistanceEF.Units;
using SecondComplexManagement.Services.Complexes;
using SecondComplexManagement.Services.Complexes.Contracts.Dto;
using SecondComplexManagement.Services.Complexes.Exceptions;
using SecondComplexManagement.Services.Contracts;
using SecondComplexManagement.Services.Unit.Test.Factories;
using TaavArchitecture.TestTools.Infrastructure.DataBaseConfig;
using TaavArchitecture.TestTools.Infrastructure.DataBaseConfig.Unit;

namespace SecondComplexManagement.Services.Unit.Test.Complexes
{
    public class ComplexServiceTests : BusinessUnitTest
    {
        private readonly ComplexAppService _sut;
        public ComplexServiceTests()
        {
            var complexRepository = new EFComplexRepository(SetupContext);
            var unitOfWork = new EFUnitOfWork(SetupContext);
            var unitRepository = new EFUnitRepository(SetupContext);
            _sut = new ComplexAppService(
                complexRepository,
                unitOfWork,
                unitRepository);
        }

        [Fact]
        public void Add_add_complex_properly()
        {
            //Arrange
            var complex = new AddComplexDto
            {
                Name = "dummy",
                UnitCount = 20
            };

            //Act
            _sut.Add(complex);

            //Assert
            var expected = ReadContext.Set<Complex>().Single();
            expected.Name.Should().Be("dummy");
            expected.UnitCount.Should().Be(20);
        }

        [Fact]
        public void UpdateUnitCount_update_unit_count_properly()
        {
            //Arrange
            var complex = ComplexFactory.CreateComplex();
            DbContext.Save(complex);
            var unitCount = 8;

            _sut.UpdateUnitCount(complex.Id, unitCount);

            var expected = ReadContext.Set<Complex>().Single();
            expected.UnitCount.Should().Be(unitCount);
        }

        [Fact]
        public void UpdateUnitCount_throw_exception_when_complex_has_unit()
        {
            var complex = ComplexFactory.CreateComplex();
            var unitCount = 8;
            var block = BlockFactory.CreateBlock(complex);
            var unit = new Entities.Unit
            {
                Block = block,
                Name = "dummy",
                ResidenceType = ResidenceType.Owner
            };
            DbContext.Save(unit);

            var expected = () => _sut.UpdateUnitCount(complex.Id, unitCount);

            expected.Should().ThrowExactly<ComplexHasUnitException>();

        }

        [Fact]
        public void UpdateUnitCount_throw_exception_when_complex_notFound()
        {
            var invalidComplexId = -1;
            var unitCount = 8;

            var expected = () => _sut.UpdateUnitCount(invalidComplexId, unitCount);

            expected.Should().ThrowExactly<ComplexNotFoundException>();
        }

        [Fact]
        public void GetAll_get_all_complexes_properly()
        {
            //Arrange
            var complex = ComplexFactory.CreateComplex();

            var block = BlockFactory.CreateBlock(complex);

            var unit = new Entities.Unit
            {
                Block = block,
                Name = "dummy",
                ResidenceType = ResidenceType.Owner
            };

            var unit2 = new Entities.Unit
            {
                Block = block,
                Name = "dummy2",
                ResidenceType = ResidenceType.Owner
            };

            DbContext.Save(unit);
            DbContext.Save(unit2);

            //Act
            var expected = _sut.GetAll(null).Single();

            //Assert
            expected.Id.Should().Be(complex.Id);
            expected.Name.Should().Be(complex.Name);
            expected.AddedUnitCount.Should().Be(2);
            expected.RemainedUnitsCount.Should().Be(18);

        }

        [Fact]
        public void GetAllComplexes_get_all_with_search_Name()
        {
            //Arrange
            var complex = ComplexFactory.CreateComplex();
            var complex2 = ComplexFactory.CreateComplex("dummy2", 25);
            var block2 = BlockFactory.CreateBlock(complex2);

            var unit = new Entities.Unit
            {
                Block = block2,
                Name = "dummy",
                ResidenceType = ResidenceType.Owner
            };

            var unit2 = new Entities.Unit
            {
                Block = block2,
                Name = "dummy2",
                ResidenceType = ResidenceType.Owner
            };

            DbContext.Save(unit);
            DbContext.Save(unit2);

            var searchName = "2";

            //Act
            var expected = _sut.GetAll(searchName).Single();


            //Assert
            expected.Name.Should().Be("dummy2");
            expected.Id.Should().Be(complex2.Id);
            expected.AddedUnitCount.Should().Be(2);
            expected.RemainedUnitsCount.Should().Be(23);

        }

        [Fact]
        public void GetComplexById_get_complex_by_id_without_search()
        {
            var complex = ComplexFactory.
                CreateComplex();
            var complex2 = ComplexFactory.
                CreateComplex("dummy2", 50);

            var block = BlockFactory
                .CreateBlock(complex);
            var unit = new Entities.Unit
            {
                Block = block,
                Name = "dummy",
                ResidenceType = ResidenceType.Owner
            };

            var unit2 = new Entities.Unit
            {
                Block = block,
                Name = "dummy2",
                ResidenceType = ResidenceType.Owner
            };

            DbContext.Save(unit);
            DbContext.Save(unit2);

            //Act
            var expected = _sut.GetById(complex.Id);

            expected.Name.Should().Be(complex.Name);
            expected.Id.Should().Be(complex.Id);
            expected.AddedUnitsCount.Should().Be(2);
            expected.RemainedUnitsCount.Should().Be(18);
            expected.AddedBlocksCount.Should().Be(1);
        }

        [Fact]
        public void GetById_when_return_null_when_id_not_found()
        {
            var invalidComplexId = -1;
            var expected = _sut.GetById(invalidComplexId);

            expected.Should().BeNull();
        }

        [Fact]
        public void GetByIdWithBlocks_get_by_id_properly()
        {
            var complex = ComplexFactory.
                CreateComplex();
            var complex2 = ComplexFactory.
                CreateComplex("dummy2", 50);

            var block = BlockFactory
                .CreateBlock(complex);
            var unit = new Entities.Unit
            {
                Block = block,
                Name = "dummy",
                ResidenceType = ResidenceType.Owner
            };

            var unit2 = new Entities.Unit
            {
                Block = block,
                Name = "dummy2",
                ResidenceType = ResidenceType.Owner
            };
            DbContext.Save(unit);
            DbContext.Save(unit2);

            var expected = _sut.GetByIdWithBlocks(complex.Id, null);
            expected.Name.Should().Be(complex.Name);
            expected.Id.Should().Be(complex.Id);
            var singleBlock = expected.Blocks.Single();
            singleBlock.Name.Should().Be(block.Name);
            singleBlock.AddedUnitsCount.Should().Be(2);
        }


        [Fact]
        public void GetByIdWithBlocks_return_null_when_id_not_found()
        {
            var invalidComplexId = -1;
            var expected = _sut.GetByIdWithBlocks(invalidComplexId, null);

            expected.Should().BeNull();
        }

        [Fact]
        public void GetByIdWithBlocks_search_in_block_name()
        {
            var complex = ComplexFactory.
                CreateComplex();
            var complex2 = ComplexFactory.
                CreateComplex("dummy2", 50);

            var block = BlockFactory
                .CreateBlock(complex);
            var block2 = BlockFactory.
                CreateBlock(complex, "dummy2");
            var unit = new Entities.Unit
            {
                Block = block,
                Name = "dummy",
                ResidenceType = ResidenceType.Owner
            };

            var unit2 = new Entities.Unit
            {
                Block = block,
                Name = "dummy2",
                ResidenceType = ResidenceType.Owner
            };

            var unit3 = new Entities.Unit
            {
                Block = block2,
                Name = "dummy2",
                ResidenceType = ResidenceType.Owner
            };

            DbContext.Save(unit);
            DbContext.Save(unit2);
            DbContext.Save(unit3);

            var blockNameSearch = "2";
            //Act 
            var expected = _sut.GetByIdWithBlocks(
                complex.Id,
                blockNameSearch);

            var singleBlock = expected.Blocks.Single();
            singleBlock.Name.Should().Be("dummy2");
            singleBlock.AddedUnitsCount.Should().Be(1);

        }

        [Fact]
        public void Delete_delete_properly()
        {
            var complex = ComplexFactory
                .CreateComplex();

            var block = BlockFactory
                .CreateBlock(complex);
            DbContext.Save(block);

            _sut.Delete(complex.Id);

            ReadContext.Set<Complex>().Should().HaveCount(0);
            ReadContext.Set<Block>().Should().HaveCount(0);
        }

        [Fact]
        public void Delete_throw_exception_when_complex_not_found()
        {
            var invalidComplexId = -1;
            var expected = () => _sut.Delete(invalidComplexId);
            expected.Should()
                .ThrowExactly<ComplexNotFoundException>();
        }

        [Fact]
        public void Delete_throw_exception_when_complex_has_unit()
        {

            var complex = ComplexFactory
                .CreateComplex();

            var block = BlockFactory
                .CreateBlock(complex);
            var unit = UnitFactory
                .CreateUnit(block);

            DbContext.Save(unit);

            var expected = () => _sut.Delete(complex.Id);

            expected.Should()
                .ThrowExactly<ComplexHasUnitException>();
        }

    }
}
