using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalAPI.ViewModels
{
    public class RegisterInput
    {
        public String UserName { get; set; }

        public String Telephone { get; set; }

        public String Password { get; set; }

        public String IdCard { get; set; }
    }
}
