using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalAPI.Applications.Doctors.Dtos
{
    public class DoctorListDto
    {
        public int Id { get; set; }

        public string DoctorName { get; set; }

        public string PictureLink { get; set; }

        public string Resume { get; set; }

        public string DepartmentName { get; set; }

        public string HospitalName { get; set; }

        public int DepartmentId { get; set; }

        public int HospitalId { get; set; }
    }
}
