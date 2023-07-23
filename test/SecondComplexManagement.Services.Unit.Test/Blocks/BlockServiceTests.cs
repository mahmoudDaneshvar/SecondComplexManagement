using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using SecondComplexManagement.Entities;
using SecondComplexManagement.PersistanceEF;
using SecondComplexManagement.PersistanceEF.Blocks;
using SecondComplexManagement.PersistanceEF.Units;
using SecondComplexManagement.Services.Blocks;
using SecondComplexManagement.Services.Blocks.Contracts;
using SecondComplexManagement.Services.Blocks.Contracts.Dto;
using SecondComplexManagement.Services.Blocks.Exceptions;
using SecondComplexManagement.Services.Complexes.Exceptions;
using SecondComplexManagement.Services.Contracts;
using SecondComplexManagement.Services.Unit.Test.Complexes;
using SecondComplexManagement.Services.Unit.Test.Factories;
using SecondComplexManagement.Services.Units.Contracts.Dto;
using SecondComplexManagement.Services.Units.Exceptions;
using System.Numerics;
using TaavArchitecture.TestTools.Infrastructure.DataBaseConfig;
using TaavArchitecture.TestTools.Infrastructure.DataBaseConfig.Unit;
using Complex = SecondComplexManagement.Entities.Complex;

namespace SecondComplexManagement.Services.Unit.Test.Blocks
{
    public class BlockServiceTests : BusinessUnitTest
    {
        private readonly BlockAppService _sut;
        public BlockServiceTests()
        {
            var blockRepository = new EFBlockRepository(SetupContext);
            var unitOfWork = new EFUnitOfWork(SetupContext);
            var complexRepository = new EFComplexRepository(SetupContext);
            var unitRepository = new EFUnitRepository(SetupContext);
            _sut = new BlockAppService(
                blockRepository,
                unitOfWork,
                complexRepository,
                unitRepository);
        }

        [Fact]
        public void Add_add_block_properly()
        {

            var complex = new Complex
            {
                Name = "dummy",
                UnitCount = 20
            };

            DbContext.Save(complex);

            var block = new AddBlockDto
            {
                ComplexId = complex.Id,
                Name = "dummy",
                UnitCount = 5
            };
            _sut.Add(block);

            var expected = ReadContext.Set<Block>().Single();
            expected.Name.Should().Be("dummy");
            expected.UnitCount.Should().Be(5);
            expected.ComplexId.Should().Be(complex.Id);
        }

        [Fact]
        public void Add_throw_exception_when_duplicate_block_name()
        {
            var complex = new Complex
            {
                Name = "dummy",
                UnitCount = 20
            };

            DbContext.Save(complex);

            var block = new AddBlockDto
            {
                ComplexId = complex.Id,
                Name = "dummy",
                UnitCount = 5
            };
            _sut.Add(block);

            var block2 = new AddBlockDto
            {
                Name = "dummy",
                UnitCount = 5,
                ComplexId = complex.Id

            };

            var expected = () => _sut.Add(block2);

            expected.Should()
                .ThrowExactly<DuplicateBlockNameInSameComplexException>();
        }

        [Fact]
        public void
            Add_throw_exception_when_block_unit_count_greater_than_complex_unit_count()
        {
            var complex = new Complex
            {
                Name = "dummy",
                UnitCount = 20
            };

            DbContext.Save(complex);

            var block = new AddBlockDto
            {
                ComplexId = complex.Id,
                Name = "dummy",
                UnitCount = 25
            };
            var expected = () => _sut.Add(block);

            expected.Should()
                .ThrowExactly<BlockUnitCountOutOfRangeException>();

        }

        [Fact]
        public void Add_throw_exception_when_complex_not_found()
        {
            var invalidComplexId = -1;

            var block = new AddBlockDto
            {
                ComplexId = invalidComplexId,
                Name = "dummy",
                UnitCount = 25
            };

            var expected = () => _sut.Add(block);

            expected.Should()
                .ThrowExactly<ComplexNotFoundException>();
        }

        [Fact]
        public void
            Add_throw_exception_when_complex_is_full()
        {
            //Arrange
            var complex = new Complex
            {
                Name = "dummy",
                UnitCount = 20
            };

            DbContext.Save(complex);

            var block = new AddBlockDto
            {
                ComplexId = complex.Id,
                Name = "dummy",
                UnitCount = 15
            };
            _sut.Add(block);
            var block2 = new AddBlockDto
            {
                ComplexId = complex.Id,
                Name = "dummy2",
                UnitCount = 10
            };

            var expected = () => _sut.Add(block2);

            expected.Should()
                .ThrowExactly<ComplexIsFullException>();
        }


        [Fact]
        public void Edit_edit_block_name_and_unit_count_properly()
        {
            //Arrange
            var complex = ComplexFactory.CreateComplex();
            var block = BlockFactory.CreateBlock(complex);
            DbContext.Save(block);
            var dto = new EditBlockDto
            {
                Name = "dummy2",
                UnitCount = 3
            };

            //Act
            _sut.Update(block.Id, dto);

            //Assert
            var expected = ReadContext.Set<Block>().Single();
            expected.Name.Should().Be("dummy2");
            expected.UnitCount.Should().Be(3);
        }
        [Fact]
        public void Edit_throw_exception_when_block_not_found()
        {
            var invalidBlockId = -1;
            var dto = new EditBlockDto
            {
                Name = "dummy2",
                UnitCount = 3
            };

            var expected = () => _sut.Update(invalidBlockId, dto);
            expected.Should().ThrowExactly<BlockNotFoundException>();
        }

        [Fact]
        public void Edit_edit_just_name_when_block_has_unit()
        {
            var complex = ComplexFactory.CreateComplex();
            var block = BlockFactory.CreateBlock(complex);
            var unit = UnitFactory.CreateUnit(block);
            DbContext.Save(unit);
            var dto = new EditBlockDto
            {
                Name = "dummy2",
                UnitCount = 3
            };
            //Act
            _sut.Update(block.Id, dto);

            //Assert
            var expected = ReadContext.Set<Block>().Single();
            expected.Name.Should().Be("dummy2");
            expected.UnitCount.Should().Be(5);
        }
        [Fact]
        public void Edit_throw_exception_when_duplicate_block_name()
        {
            var complex = ComplexFactory.CreateComplex();
            var block = BlockFactory.CreateBlock(complex);
            var block2 = BlockFactory.CreateBlock(complex, name: "dummy2");
            DbContext.Save(block);
            DbContext.Save(block2);
            var dto = new EditBlockDto
            {
                Name = "dummy2",
                UnitCount = 3
            };
            //Act
            var expected = () => _sut.Update(block.Id, dto);

            expected.Should()
                .ThrowExactly<DuplicateBlockNameInSameComplexException>();

        }

        [Fact]
        public void
            Edit_edit_unit_count_when_new_name_equals_old_name()
        {
            var complex = ComplexFactory.CreateComplex();
            var block = BlockFactory.CreateBlock(complex);
            var block2 = BlockFactory.CreateBlock(complex, name: "dummy2");
            DbContext.Save(block);
            DbContext.Save(block2);
            var dto = new EditBlockDto
            {
                Name = "dummy",
                UnitCount = 3
            };

            _sut.Update(block.Id, dto);

            var expected = ReadContext.Set<Block>()
                .Where(_ => _.Name == "dummy").First();

            ReadContext.Set<Block>().Should().HaveCount(2);
            expected.Name.Should().Be("dummy");
            expected.UnitCount.Should().Be(3);
        }


        [Fact]
        public void GetAll_get_all_blocks_properly()
        {
            //Arrange
            var complex = ComplexFactory.CreateComplex();
            var block = BlockFactory.CreateBlock(complex);

            var unit = UnitFactory.CreateUnit(block);
            DbContext.Save(unit);

            var block2 = BlockFactory.CreateBlock(complex,
                name: "dummy2",
                unitCount: 6);

            var unit2 = UnitFactory.CreateUnit(block2);
            var unit3 = UnitFactory.CreateUnit(block2, "dummy2");
            DbContext.Save(unit2);
            DbContext.Save(unit3);
            //Act
            var expected = _sut.GetAll();

            expected.Should().HaveCount(2);

            var first = expected.Where(_ => _.Name == "dummy").First();
            var second = expected.Where(_ => _.Name == "dummy2").First();

            first.UnitCount.Should().Be(5);
            second.UnitCount.Should().Be(6);
            first.AddedUnitsCount.Should().Be(1);
            second.AddedUnitsCount.Should().Be(2);
            first.RemainedUnitsCount.Should().Be(4);
            second.RemainedUnitsCount.Should().Be(4);
        }

        [Fact]
        public void GetById_get_block_by_id_properly()
        {
            //Arrange
            var complex = ComplexFactory.CreateComplex();
            var block = BlockFactory.CreateBlock(complex);

            var unit = UnitFactory.CreateUnit(block);
            DbContext.Save(unit);

            var block2 = BlockFactory.CreateBlock(complex,
                name: "dummy2",
                unitCount: 6);

            var unit2 = UnitFactory.CreateUnit(block2);
            var unit3 = UnitFactory.CreateUnit(block2, "dummy2");
            DbContext.Save(unit2);
            DbContext.Save(unit3);

            //Act
            var expected = _sut.GetById(block2.Id);

            //Assert
            expected.Name.Should().Be(block2.Name);
            var units = expected.Units;
            units.Should().HaveCount(2);

        }

        [Fact]
        public void GetById_return_null_when_id_not_found()
        {
            var invalidBlockId = -1;
            var expected = _sut.GetById(invalidBlockId);

            expected.Should().BeNull();
        }
        [Fact]
        public void AddBlockWithUnits_add_with_units_properly()
        {
            var complex = ComplexFactory.CreateComplex();
            DbContext.Save(complex);
            var block = new AddBlockDto
            {
                ComplexId = complex.Id,
                Name = "dummy",
                UnitCount = 5
            };
            var units = new List<AddUnitsForBlockDto>
            {
                new AddUnitsForBlockDto
                {
                    Name = "dummy",
                    Type = ResidenceType.Owner
                },
                new AddUnitsForBlockDto
                {
                    Name = "dummy2",
                    Type = ResidenceType.Tenant
                }
            };

            var addDto = new AddBlockWithUnitsDto
            {
                Block = block,
                Units = units
            };

            //Act
            _sut.AddWithUnits(addDto);

            //Assert
            var expectedBlocks = ReadContext.Set<Block>()
                .Include(_ => _.Units).Single();
            expectedBlocks.Units.Should().HaveCount(2);
            expectedBlocks.Units.Should().Contain(_ => _.Name == "dummy");
            expectedBlocks.Units.Should().Contain(_ => _.Name == "dummy2");

            var expectedUnits = ReadContext.Set<Entities.Unit>();

            expectedUnits.Should().HaveCount(2);
        }

        [Fact]
        public void AddWithUnits_throw_exception_when_duplicate_block_name()
        {
            var complex = ComplexFactory.CreateComplex();
            var block2 = BlockFactory.CreateBlock(complex);
            DbContext.Save(block2);
            var block = new AddBlockDto
            {
                ComplexId = complex.Id,
                Name = "dummy",
                UnitCount = 5
            };
            var units = new List<AddUnitsForBlockDto>
            {
                new AddUnitsForBlockDto
                {
                    Name = "dummy",
                    Type = ResidenceType.Owner
                }
            };
            var addDto = new AddBlockWithUnitsDto
            {
                Block = block,
                Units = units
            };

            var expected = () => _sut.AddWithUnits(addDto);
            expected.Should()
                .ThrowExactly<DuplicateBlockNameInSameComplexException>();
        }

        [Fact]
        public void AddWithUnits_throw_exception_when_duplicate_unit_name()
        {
            var complex = ComplexFactory.CreateComplex();
            DbContext.Save(complex);
            var block = new AddBlockDto
            {
                ComplexId = complex.Id,
                Name = "dummy",
                UnitCount = 5
            };
            var units = new List<AddUnitsForBlockDto>
            {
                new AddUnitsForBlockDto
                {
                    Name = "dummy",
                    Type = ResidenceType.Owner
                },
                new AddUnitsForBlockDto
                {
                    Name = "dummy",
                    Type = ResidenceType.Tenant
                }
            };

            var addDto = new AddBlockWithUnitsDto
            {
                Block = block,
                Units = units
            };

            var expected = () => _sut.AddWithUnits(addDto);

            expected.Should()
                .ThrowExactly<DuplicateUnitNameInSameBlockException>();
        }
        [Fact]
        public void AddWithUnits_throw_exception_when_block_is_full()
        {
            var complex = ComplexFactory.CreateComplex();
            DbContext.Save(complex);
            var block = new AddBlockDto
            {
                ComplexId = complex.Id,
                Name = "dummy",
                UnitCount = 1
            };
            var units = new List<AddUnitsForBlockDto>
            {
                new AddUnitsForBlockDto
                {
                    Name = "dummy",
                    Type = ResidenceType.Owner
                },
                new AddUnitsForBlockDto
                {
                    Name = "dummy2",
                    Type = ResidenceType.Tenant
                }
            };

            var addDto = new AddBlockWithUnitsDto
            {
                Block = block,
                Units = units
            };

            var expected = () => _sut.AddWithUnits(addDto);
            expected.Should()
                .ThrowExactly<BlockIsFullException>();
        }
        [Fact]
        public void AddWithUnits_throw_exception_when_complex_is_full()
        {
            var complex = ComplexFactory
                .CreateComplex(unitCount: 5);
            var block = BlockFactory
                .CreateBlock(complex, unitCount: 5);
            DbContext.Save(block);
            var block2 = new AddBlockDto
            {
                ComplexId = complex.Id,
                Name = "dummy2",
                UnitCount = 1
            };
            var units = new List<AddUnitsForBlockDto>
            {
                new AddUnitsForBlockDto
                {
                    Name = "dummy",
                    Type = ResidenceType.Owner
                },
                new AddUnitsForBlockDto
                {
                    Name = "dummy2",
                    Type = ResidenceType.Tenant
                }
            };

            var addDto = new AddBlockWithUnitsDto
            {
                Block = block2,
                Units = units
            };

            var expected = () => _sut.AddWithUnits(addDto);
            expected.Should()
                .ThrowExactly<ComplexIsFullException>();

        }

        [Fact]
        public void Delete_delete_properly()
        {
            var complex = ComplexFactory
                .CreateComplex();
            var block = BlockFactory
                .CreateBlock(complex);
            DbContext.Save(block);
            //Act
            _sut.Delete(block.Id);

            ReadContext.Set<Complex>().Should().HaveCount(1);
            ReadContext.Set<Block>().Should().HaveCount(0);
        }

        [Fact]
        public void Delete_throw_exception_when_block_not_found()
        {
            var invalidBlockId = -1;
            var expected = () => _sut.Delete(invalidBlockId);

            expected.Should()
                .ThrowExactly<BlockNotFoundException>();
        }

        [Fact]
        public void Delete_throw_exception_when_block_has_unit()
        {
            var complex = ComplexFactory
                .CreateComplex();
            var block = BlockFactory
                .CreateBlock(complex);
            var unit = UnitFactory
                .CreateUnit(block);
            DbContext.Save(unit);

            var expected =() => _sut.Delete(block.Id);

            expected.Should()
                .ThrowExactly<BlockHasUnitException>();
        }
    }
}
