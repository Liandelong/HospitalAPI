using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalAPI.Applications.Answers
{
    public class ChatMessagesDto
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public int Type { get; set; }

        public string Pic { get; set; }
    }
}
