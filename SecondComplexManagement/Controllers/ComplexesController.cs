﻿using Microsoft.AspNetCore.Mvc;
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

    }
}
