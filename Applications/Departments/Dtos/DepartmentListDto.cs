using HospitalAPI.Applications.commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalAPI.Applications.Departments.Dtos
{
    public class DepartmentListDto
    {
        public int Id { get; set; }
        public String DepartmentName { get; set; }
    }
}
