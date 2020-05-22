using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalAPI.Applications.Departments.Dtos
{
    public class CreateOrEditDepartmentDto
    {
        public int? Id { get; set; }
        public String DepartmentName { get; set; }
    }
}
