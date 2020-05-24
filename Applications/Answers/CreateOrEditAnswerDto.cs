using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalAPI.Applications.Answers
{
    public class CreateOrEditAnswerDto
    {
        public int? Id { get; set; }

        public int UserId { get; set; }

        public int ProblemId { get; set; }

        public string Content { get; set; }
    }
}
