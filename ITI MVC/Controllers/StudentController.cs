using ITI_MVC.Context;
using ITI_MVC.CustomActionFilters;
using ITI_MVC.Interfaces;
using ITI_MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ITI_MVC.Controllers
{
    [Authorize(Roles = "Admin, Student")]
    [LogFilter]
    public class StudentController : Controller
    {
        private readonly IStudentInterface StudentRepo;
        private readonly IDepartmentInterface DepartmentRepo;

        public StudentController(IStudentInterface _StudentRepo, IDepartmentInterface _DepartmentRepo)
        {
            StudentRepo = _StudentRepo;
            DepartmentRepo = _DepartmentRepo;
        }

        ITIContext db = new ITIContext();

        public IActionResult Index()
        {
            var students = StudentRepo.GetAll();
            return View(students);
        }


        public IActionResult Details(int id)
        {
            Student Std = StudentRepo.GetById(id);
            if (Std == null)
            {
                return NotFound();
            }
            else
            {
                return View(Std);
            }
        }

        public IActionResult StudentForm()
        {
            var depts = DepartmentRepo.GetAll();
            SelectList sl = new(depts, "ID", "Name");
            ViewBag.depts = sl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(IFormFile? stdphoto, Student std)
        {
            Guid guid = Guid.NewGuid();

            if(stdphoto != null)
            {
                string FileExtension = stdphoto.FileName.Split('.').Last();
                string FilePath = $"wwwroot/images/{guid}.{FileExtension}";
                using (FileStream st = new FileStream(FilePath, FileMode.Create))
                {
                    await stdphoto.CopyToAsync(st);
                }

                std.ImgPath = $"{guid}.{FileExtension}";
            }


            if (ModelState.IsValid)
            {
                StudentRepo.Add(std);
                return RedirectToAction("Index");
            }
            else
            {
                var depts = db.Departments.ToList();
                SelectList sl = new(depts, "ID", "Name");
                ViewBag.depts = sl;
                return View("StudentForm", std);
            }
        }

        public IActionResult CheckEmailExist(int id, string Email, string Name, string Age)
        {
            var res = db.Students.FirstOrDefault(s => s.Email == Email);
            if (res != null && res.ID != id)
            {
                return Json($"Email Already Taken. Try {Name}{Age}");
            }
            return Json(true);
        }

        public IActionResult Update(int id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            else
            {
                var depts = DepartmentRepo.GetAll();
                SelectList sl = new(depts, "ID", "Name");
                ViewBag.depts = sl;
                var std = StudentRepo.GetById(id);
                return View(std);
            }
        }
        [HttpPost]
        public IActionResult Update(int id, Student student)
        {
            StudentRepo.Update(id, student);
            return RedirectToAction("Index");
        }

        public IActionResult RemoveForm(int id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            //Student std = db.Students.Include(s => s.Department).SingleOrDefault(s => s.ID == id);
            Student std = StudentRepo.GetById(id);

            if (std == null)
            {
                return NotFound();
            }
            else
            {
                return View(std);
            }
        }

        public IActionResult Remove(int id)
        {
            StudentRepo.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
