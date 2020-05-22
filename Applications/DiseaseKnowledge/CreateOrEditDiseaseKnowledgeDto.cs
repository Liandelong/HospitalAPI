using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalAPI.Applications.DiseaseKnowledge
{
    public class CreateOrEditDiseaseKnowledgeDto
    {
        public int? Id { get; set; }

        public DateTime DateTime { get; set; }

        public int Article_Type { get; set; }

        public string Content { get; set; }

        public String Title { get; set; }

        public String Image_url { get; set; }

        public String Source { get; set; }

        public bool VideoSrc { get; set; }

        public int DiseaseKindId { get; set; }
    }
}
