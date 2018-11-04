using System;
using System.Collections.Generic;
using System.Text;

namespace DYCApp.Model
{
    public class UserInfo
    {
        public int Id { get; set; }
        public string userName { get; set; }
        public string FullName { get; set; }

        public string RoleName { get; set; }

        public int FacultyId { get; set; }

        public string PhoneNumber { get; set; }

        public string DYCID { get; set; }

        public string Email { get; set; }
    }
}
