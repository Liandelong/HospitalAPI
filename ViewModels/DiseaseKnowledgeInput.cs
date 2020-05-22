using HospitalAPI.Applications.commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalAPI.ViewModels
{
    public class DiseaseKnowledgeInput : Filters
    {
        public int DiseaseKindId { get; set; }
    }
}
