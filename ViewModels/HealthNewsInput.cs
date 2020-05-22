using HospitalAPI.Applications.commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalAPI.ViewModels
{
    public class HealthNewsInput: Filters
    {
        public int NewsKindId { get; set; }
    }
}
