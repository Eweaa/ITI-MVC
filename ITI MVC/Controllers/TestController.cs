using Microsoft.AspNetCore.Mvc;

namespace ITI_MVC.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ReadArray()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ReadArray(string[] arr, IFormFile stdphoto)
        {
            Guid guid = Guid.NewGuid();
            string FileExtension = stdphoto.FileName.Split('.').Last();
            string FilePath = $"wwwroot/images/{guid}.{FileExtension}";



            using (FileStream st = new FileStream(FilePath, FileMode.Create))
            {
                await stdphoto.CopyToAsync(st);
            }

            string s = "";

            foreach (var item in arr)
            {
                s += item;
            }
            
            ViewBag.image = $"{guid}.{FileExtension}";

            return View("imgView");

        }

        public IActionResult ReadDic(Dictionary<string, int> arr) 
        {

            return View();
        }

        public IActionResult AddData(int id, string Name)
        {

            HttpContext.Session.SetInt32("id", id);
            HttpContext.Session.SetString("name", Name);
            CookieOptions cookie = new CookieOptions() { Expires=DateTime.Now.AddMinutes(1)};
            Response.Cookies.Append("sid", id.ToString(), cookie);
            Response.Cookies.Append("sname", Name);
            return Content($"{id} :: {Name}");
        }

        public IActionResult ReadData()
        {
            //int? id = HttpContext.Session.GetInt32("id");
            //string Name = HttpContext.Session.GetString("name");
            int id = int.Parse(Request.Cookies["sid"]);
            string Name = Request.Cookies["sname"];
            return Content($"{id} :: {Name}");
        }
    }
}
