﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalAPI.Applications.DiseaseKinds
{
    public class CreateOrEditDiseaseKindsDto
    {
        public int? Id { get; set; }

        public String Name { get; set; }
    }
}
