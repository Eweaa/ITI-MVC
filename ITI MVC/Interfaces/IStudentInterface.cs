using ITI_MVC.Models;

namespace ITI_MVC.Interfaces
{
    public interface IStudentInterface
    {
        public List<Student> GetAll();
        public Student GetById(int id);
        public void Add(Student student);
        public void Update(int id, Student student);
        public void Delete(int id);
    }
}
