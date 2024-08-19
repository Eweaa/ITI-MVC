using System.ComponentModel.DataAnnotations;

namespace ITI_MVC.Models
{
    public class Course
    {
        public int ID { get; set; }
        [MaxLength(20)]
        public string Name { get; set; }
        public int Duration { get; set; }
        public virtual List<Department> Departments { get; set; } = [];
    }
}
