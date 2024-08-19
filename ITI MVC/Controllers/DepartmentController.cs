using ITI_MVC.Context;
using ITI_MVC.Interfaces;
using ITI_MVC.Models;
using ITI_MVC.CustomActionFilters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace ITI_MVC.Controllers
{
    [Authorize(Roles = "Admin")]
    [ExceptionFilter]
    public class DepartmentController : Controller
    {
        private readonly IDepartmentInterface Department;
        ITIContext db = new ITIContext();

        public DepartmentController(IDepartmentInterface _Department)
        {
            Department = _Department;
        }
        
        public IActionResult Index()
        {
            var depts = Department.GetAll();
            return View(depts);
        }

        public IActionResult DepartmentForm()
        {
            var depts = Department.GetAll();
            return View(depts);
        }

        
        public IActionResult Details(int id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var Courses = db.Courses.ToList();
            //Department dept = Department.GetById(id);
            Department dept = db.Departments.Include(d => d.Courses).SingleOrDefault(d => d.ID == id);
            var CoursesNotInDept = Courses.Except(dept.Courses);
            ViewBag.CoursesNotInDept = CoursesNotInDept;

            if (dept == null)
            {
                return NotFound();
            }
            else
            {
                return View(dept);
            }
        }

        public IActionResult EditCourses(int id)
        {
            var Courses = db.Courses.ToList();
            Department dept = db.Departments.Include(d => d.Courses).SingleOrDefault(d => d.ID == id);
            var CoursesNotInDept = Courses.Except(dept.Courses);
            ViewBag.CoursesNotInDept = CoursesNotInDept;

            if (dept == null)
            {
                return NotFound();
            }
            else
            {
                return View(dept);
            }
        }

        [HttpPost]
        public IActionResult EditCourses(int id, List<int> coursestoremove, List<int> coursestoadd)
        {
            var dept = db.Departments.Include(d => d.Courses).SingleOrDefault(d => d.ID == id);

            foreach (var item in coursestoremove)
            {
                var course = db.Courses.SingleOrDefault(c => c.ID == item);
                dept.Courses.Remove(course);
            }

            
            foreach (var item in coursestoadd)
            {
                var course = db.Courses.SingleOrDefault(c => c.ID == item);
                dept.Courses.Add(course);
            }

            db.SaveChanges();

            return RedirectToAction("Details", new { id = dept.ID });

        }

        public IActionResult Create(Department dept)
        {

            if (ModelState.IsValid)
            {
                Department.Add(dept);
                return RedirectToAction("Index");
            }
            else
            {
                return View("DepartmentForm", dept);
            }
        }

        [HttpPost]
        public IActionResult Update(int id, Department NewDept)
        {
            Department.Update(id, NewDept);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            else
            {
                var dept = Department.GetById(id);
                return View(dept);
            }
        }

        public IActionResult RemoveForm(int id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            Department dept = Department.GetById(id);

            if (dept == null)
            {
                return NotFound();
            }
            else
            {
                return View(dept);
            }
        }

        public IActionResult Remove(int id)
        {
            Department.Delete(id);
            return RedirectToAction("Index");
        }
    }

}
