using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ITI_MVC.Models
{
    public class Student
    {
        public int ID { get; set; }
        [Required, MaxLength(30)]
        public string Name { get; set; }
        public string? ImgPath { get; set; }
        [Range(16, 45)]
        public int Age { get; set; }
        [Required, Remote("CheckEmailExist", "Student", AdditionalFields = "Name,Age"), RegularExpression(@"[a-zA-Z0-9_]+@[a-zA-z]+.[a-zA-Z]{2,4}")]
        public string Email { get; set; }
        public string? Phone { get; set; }
        public int DepartmentID { get; set; }
        public virtual Department? Department { get; set; }
    }
}
