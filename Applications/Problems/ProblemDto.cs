using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalAPI.Applications.Problems
{
    public class ProblemDto
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string UserName { get; set; }

        public string Title { get; set; }

        public DateTime CreateTime { get; set; }

        public int Status { get; set; }
    }
}
