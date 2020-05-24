using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalAPI.Applications.Problems
{
    public class ProblemListDto
    {
        public int Id { get; set; }

        public string Img { get; set; }

        public string Info { get; set; }

        public string Title { get; set; }

        public DateTime CreateTime { get; set; }

        public string Another { get; set; }

        public int UserId { get; set; }

        public int ProblemId { get; set; }
        //public int Status { get; set; }
    }
}
