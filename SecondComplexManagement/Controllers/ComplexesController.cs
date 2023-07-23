using Microsoft.AspNetCore.Mvc;
using SecondComplexManagement.Services.Complexes.Contracts;
using SecondComplexManagement.Services.Complexes.Contracts.Dto;

namespace SecondComplexManagement.RestApi.Controllers
{
    [ApiController]
    [Route("complexes")]
    public class ComplexesController : Controller
    {
        private readonly ComplexService _service;

        public ComplexesController(ComplexService service)
        {
            _service = service;
        }

        [HttpPost]
        public void Add(AddComplexDto dto)
        {
            _service.Add(dto);
        }

        [HttpPatch]
        [Route("{id}/unit-count")]
        public void UpdateUnitCount([FromRoute]int id,[FromBody] int unitCount)
        {
            _service.UpdateUnitCount(id, unitCount);
        }

        [HttpGet]
        public List<GetAllComplexesDto> GetAll(
            string? searchName)
        {
           return _service.GetAll(searchName);
        }

        [HttpGet]
        [Route("{id}")]
        public GetComplexByIdDto? GetById([FromRoute]int id)
        {
            return _service.GetById(id);
        }


        [HttpGet]
        [Route("{id}/blocks")]
        public GetComplexByIdWithBlocksDto? GetByIdWithBlocks(
            [FromRoute]int id,
            string? blockNameSearch)
        {
            return _service.GetByIdWithBlocks(id, blockNameSearch);
        }
    }
}
