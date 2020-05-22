using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalAPI.Applications.User.Dtos
{
    public class UserListDto
    {
        public int Id { get; set; }

        public String UserName { get; set; }

        public String Telephone { get; set; }

        public String Password { get; set; }

        public String IdCard { get; set; }

        public bool IsAdmin { get; set; }
    }
}
