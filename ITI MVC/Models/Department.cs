using System.ComponentModel.DataAnnotations;

namespace ITI_MVC.Models
{
    public class Department
    {
        public int ID { get; set; }
        [MaxLength(30), Required]
        public string Name { get; set; }
        public virtual ICollection<Student> Students { get; set; } = new HashSet<Student>();
        public virtual ICollection<Instructor> Instructors { get; set; } = new HashSet<Instructor>();
        public virtual List<Course> Courses { get; set; } = [];
    }
}
