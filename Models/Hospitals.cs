using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalAPI.Models
{
    public class Hospitals
    {
        public int Id { get; set; }

        public String HospitalName { get; set; }

        public String HospitalAddress { get; set; }

        public String PictureLink { get; set; }

        public String Resume { get; set; }
    }
}
