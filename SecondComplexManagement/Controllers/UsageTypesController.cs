using Microsoft.AspNetCore.Mvc;
using SecondComplexManagement.Services.UsageTypes.Contracts;
using SecondComplexManagement.Services.UsageTypes.Contracts.Dto;

namespace SecondComplexManagement.RestApi.Controllers
{
    [Route("usage-types")]
    [ApiController]
    public class UsageTypesController : Controller
    {
        private readonly UsageTypeService _service;

        public UsageTypesController(UsageTypeService service)
        {
            _service = service;
        }

        [HttpPost]
        public void Add(AddUsageTypeDto dto)
        {
            _service.Add(dto);
        }
    }
}
