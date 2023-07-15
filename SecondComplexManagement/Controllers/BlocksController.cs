using Microsoft.AspNetCore.Mvc;
using SecondComplexManagement.Services.Blocks;
using SecondComplexManagement.Services.Blocks.Contracts;
using SecondComplexManagement.Services.Blocks.Contracts.Dto;

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

        [HttpPatch]
        [Route("{id}")]
        public void Update([FromRoute] int id,
            [FromBody] UpdateBlockDto dto)
        {
            _service.Update(id, dto);
        }

        [HttpGet]
        public List<GetAllBlocksDto> GetAll()
        {
            return _service.GetAll();
        }

        [HttpPost]
        [Route("with-units")]
        public void AddWithUnits(AddBlockWithUnitsDto dto)
        {
            _service.AddWithUnits(dto);
        }
    }
}
