using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalAPI.Models
{
    public class Answers
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int ProblemId { get; set; }

        public string Content { get; set; }

        public DateTime CreateTime { get; set; }

        public int Type { get; set; }
    }
}
