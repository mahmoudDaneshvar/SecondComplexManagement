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
    }
}
