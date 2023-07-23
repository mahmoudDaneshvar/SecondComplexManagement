﻿using SecondComplexManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondComplexManagement.Services.Units.Contracts.Dto
{
    public class AddUnitDto
    {
        public string Name { get; set; }
        public int BlockId { get; set; }
        public ResidenceType ResidenceType { get; set; }
    }
}
