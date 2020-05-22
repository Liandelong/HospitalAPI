using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalAPI.Applications.DiseaseKnowledge
{
    public class DiseaseKnowledgeListDto
    {
        public int Id { get; set; }

        public string DateTime { get; set; }

        public int Article_Type { get; set; }

        public string Content { get; set; }

        public String Title { get; set; }

        public String Image_url { get; set; }

        public String Source { get; set; }

        public bool VideoSrc { get; set; }

        public string DiseaseKindName { get; set; }

        public int DiseaseKindId { get; set; }
    }
}
