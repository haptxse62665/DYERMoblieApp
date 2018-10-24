using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace StudentApp.Models
{
    public class Student
    {
        string UserID {get; set;}
        string Email { get; set; }
        int PhoneNumber { get; set; }
        string FullName { get; set; }
        string FacultyID { get; set; }
        int NewPhoneNumber { get; set; }

        public Student(string userID, string email, int phoneNumber, string fullName, string facultyId, int newPhoneNumber )
        {
            UserID = userID;
            Email = email;
            PhoneNumber = phoneNumber;
            FullName = fullName;
            FacultyID = facultyId;
            NewPhoneNumber = newPhoneNumber;

        }
    }
}
