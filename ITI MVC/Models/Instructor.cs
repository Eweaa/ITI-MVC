using System.ComponentModel.DataAnnotations;

namespace ITI_MVC.Models
{
    public class Instructor
    {
        public int ID { get; set; }
        [MaxLength(20)]
        public string Name { get; set; }
        public int Salary { get; set; }
        public virtual Department? Department { get; set; }
        public int DepartmentID { get; set; }
    }
}
