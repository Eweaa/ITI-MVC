using ITI_MVC.Context;
using ITI_MVC.Interfaces;
using ITI_MVC.Models;
using Microsoft.EntityFrameworkCore;

namespace ITI_MVC.Repositories
{
    public class StudentRepo : IStudentInterface
    {
        ITIContext db = new ITIContext();

        public void Add(Student student)
        {
            if(student.ImgPath is null ||  student.ImgPath.Length == 0)
            {
                student.ImgPath = "3177440.png";
            }
            db.Students.Add(student);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var std = GetById(id);
            db.Students.Remove(std);
            db.SaveChanges();
        }

        public List<Student> GetAll()
        {
            var res = db.Students.ToList();
            return res;
        }

        public Student GetById(int id)
        {
            var res = db.Students.SingleOrDefault(s => s.ID == id);
            return res;
        }

        public void Update(int id, Student student)
        {
            Student std = GetById(id);

            if(student.Name == "" || student.Name is null)
            {
            }
            else
            {
                std.Name = student.Name;
            }
            
            if(student.Email == "" || student.Email is null)
            {
            }
            else
            {
                std.Email = student.Email;
            }

            if(student.Phone == "" || student.Phone is null)
            {
            }
            else
            {
                std.Phone = student.Phone;
            }

            if(student.ImgPath == "" || student.ImgPath is null)
            {
            }
            else
            {
                std.ImgPath = student.ImgPath;
            }

            db.Update(std);
            db.SaveChanges();

        }
    }
}
