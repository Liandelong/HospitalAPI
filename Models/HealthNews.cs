using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalAPI.Models
{
    public class HealthNews
    {
        public int Id { get; set; }

        public DateTime DateTime { get; set; }

        public int Article_Type { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string Image_url { get; set; }

        public string Source { get; set; }

        public bool VideoSrc { get; set; }

        public int NewsKindId { get; set; }
    }
}
