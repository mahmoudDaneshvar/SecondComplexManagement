using Microsoft.AspNetCore.Mvc;
using SecondComplexManagement.Services.Blocks.Contracts;
using SecondComplexManagement.Services.Blocks.Contracts.Dto;
using SecondComplexManagement.Services.Units.Contracts.Dto;

namespace SecondComplexManagement.RestApi.Controllers
{
    [ApiController]
    [Route("blocks")]
    public class BlocksController : Controller
    {
        private readonly BlockService _service;

        public BlocksController(BlockService service)
        {
            _service = service;
        }

        [HttpPost]
        public void Add(AddBlockDto dto)
        {
            _service.Add(dto);
        }
        [HttpPut]
        [Route("{id}")]
        public void Update([FromRoute] int id, EditBlockDto dto)
        {
            _service.Update(id, dto);
        }

        [HttpGet]
        public List<GetAllBlocksDto> GetAll()
        {
            return _service.GetAll();
        }
        [HttpGet]
        [Route("{id}")]
        public GetBlockByIdDto? GetById([FromRoute]int id)
        {
            return _service.GetById(id);
        }

        [HttpPost]
        [Route("with-units")]
        public void AddWithUnits(AddBlockWithUnitsDto dto)
        {
            _service.AddWithUnits(dto);
        }
    }
}
