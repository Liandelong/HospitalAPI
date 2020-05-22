using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalAPI.Models
{
    public class Doctors
    {
        public int Id { get; set; }

        public String DoctorName { get; set; }

        public String PictureLink { get; set; }

        public String Resume { get; set; }

        public int DepartmentId { get; set; }

        public int HospitalId { get; set; }
    }
}
