using System;
using System.Collections.Generic;
using System.Text;

namespace DYCApp.Model
{
    public class Responses
    {
        public string NotifiTitle { get; set; }
        public int CountNumberOK { get; set; }
        public int CountNumberNotOK { get; set; }
        public int CountWaiting { get; set; }
        public int Total { get; set; }
        public string CountryName { get; set; }
        public DateTime DateCreate { get; set; }
        public string HostName { get; set; }
        public int HostID { get; set; }
    }
}
