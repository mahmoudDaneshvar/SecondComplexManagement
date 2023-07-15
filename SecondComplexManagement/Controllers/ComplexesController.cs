using Microsoft.AspNetCore.Mvc;
using SecondComplexManagement.Services.Complexes;
using SecondComplexManagement.Services.Complexes.Contracts;
using SecondComplexManagement.Services.Complexes.Contracts.Dto;

namespace SecondComplexManagement.RestApi.Controllers
{
    [Route("complexes")]
    [ApiController]
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

        [HttpGet]
        public List<GetAllComplexesDto> GetAll(
            [FromQuery] string? name, [FromQuery] int? id)
        {
            return _service.GetAll(name, id);
        }

        [HttpGet]
        [Route("{id}")]
        public GetComplexByIdDto GetById(int id)
        {
            return _service.GetById(id);
        }

        [HttpGet]
        [Route("{id}/blocks")]
        public GetComplexByIdWithBlocksDto?
            GetByIdWithBlocks([FromRoute] int id, string? blockName)
        {
            return _service.
                GetByIdWithBlocks(id, blockName);
        }

    }
}
