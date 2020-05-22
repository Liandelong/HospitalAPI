using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalAPI.Applications.Hospitals.Dtos
{
    public class CreateOrEditHospitalDto
    {
        public int? Id { get; set; }

        public String HospitalName { get; set; }

        public String HospitalAddress { get; set; }

        public String PictureLink { get; set; }

        public String Resume { get; set; }
    }
}
