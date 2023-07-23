using Microsoft.AspNetCore.Mvc;
using SecondComplexManagement.Services.Units.Contracts;
using SecondComplexManagement.Services.Units.Contracts.Dto;

namespace SecondComplexManagement.RestApi.Controllers
{
    [ApiController]
    [Route("units")]
    public class UnitsController : Controller
    {
        private readonly UnitService _service;
        public UnitsController(UnitService unitService)
        {
            _service = unitService;
        }
        [HttpPost]
        public void Add(AddUnitDto dto)
        {
            _service.Add(dto);
        }
    }
}
