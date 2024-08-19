using ITI_MVC.Models;

namespace ITI_MVC.Interfaces
{
    public interface IDepartmentInterface
    {
        public List<Department> GetAll();
        public Department GetById(int id);
        public void Add(Department department);
        public void Update(int id, Department department);
        public void Delete(int id);
    }
}
