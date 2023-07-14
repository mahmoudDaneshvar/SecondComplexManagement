using Microsoft.AspNetCore.Mvc;
using SecondComplexManagement.Services.Units.Contracts;
using SecondComplexManagement.Services.Units.Contracts.Dto;

namespace SecondComplexManagement.RestApi.Controllers
{
    [Route("units")]
    [ApiController]
    public class UnitsController : Controller
    {
        private readonly UnitService _service;
        public UnitsController(UnitService service)
        {
            _service = service;
        }

        [HttpPost]
        public void Add(AddUnitDto dto)
        {
            _service.Add(dto);
        }
    }
}
