﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondComplexManagement.Services.Blocks.Contracts.Dto
{
    public class AddBlockDto
    {
        public string Name { get; set; }
        public int UnitCount { get; set; }
        public int ComplexId { get; set; }
    }
}
