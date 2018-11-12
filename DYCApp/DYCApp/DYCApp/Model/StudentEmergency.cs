using System;
using System.Collections.Generic;
using System.Text;

namespace DYCApp.Model
{
    public class StudentEmergency
    {
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public int StudentID { get; set; }
        public string Coordinate { get; set; }
        public string UserName { get; set; }

        public int ID { get; set; }

        public DateTime DateCreate { get; set; }
    }
}
