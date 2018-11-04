using System;
using System.Collections.Generic;
using System.Text;

namespace StudentApp.Models
{
    public class Notification
    {
        public int ID { get; set; }
        

        public string TitleNotification { get; set; }

        public string LevelEmergency { get; set; }
        public DateTime? DateCreated { get; set; }
        public string Location { get; set; }
    }
}
