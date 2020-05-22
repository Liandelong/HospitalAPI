using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalAPI.Applications.Doctors.Dtos
{
    public class CreateOrEditDoctorDto
    {
        public int? Id { get; set; }

        public string DoctorName { get; set; }

        public string PictureLink { get; set; }

        public string Resume { get; set; }

        public int DepartmentId { get; set; }

        public int HospitalId { get; set; }
    }
}
