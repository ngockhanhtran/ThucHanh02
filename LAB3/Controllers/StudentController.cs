using LAB2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LAB2.Controllers
{
    [Route("Admin/Student")]
    public class StudentController : Controller
    {
        private static List<Student> listStudents = new List<Student>()
        {
            new Student() { Id = 101, Name = "Hải Nam", Branch = Branch.IT, Gender = Gender.Male, IsRegular = true, Address = "A1-2018", Email = "nam@g.com" },
            new Student() { Id = 102, Name = "Minh Tú", Branch = Branch.BE, Gender = Gender.Female, IsRegular = true, Address = "A1-2019", Email = "tu@g.com" },
            new Student() { Id = 103, Name = "Hoàng Phong", Branch = Branch.CE, Gender = Gender.Male, IsRegular = false, Address = "A1-2020", Email = "phong@g.com" },
            new Student() { Id = 104, Name = "Xuân Mai", Branch = Branch.EE, Gender = Gender.Female, IsRegular = false, Address = "A1-2021", Email = "mai@g.com" }
        };

        // URL: /Admin/Student/List (hoặc /Admin/Student/Index)
        [Route("List")]
        [Route("Index")]
        public IActionResult Index()
        {
            return View(listStudents);
        }

        // URL: /Admin/Student/Add (GET)
        [HttpGet("Add")]
        public IActionResult Create()
        {
            ViewBag.AllGenders = Enum.GetValues(typeof(Gender)).Cast<Gender>().ToList();
            ViewBag.AllBranches = new List<SelectListItem>()
            {
                new SelectListItem { Text = "IT", Value = "IT" },
                new SelectListItem { Text = "BE", Value = "BE" },
                new SelectListItem { Text = "CE", Value = "CE" },
                new SelectListItem { Text = "EE", Value = "EE" }
            };
            return View();
        }

        // URL: /Admin/Student/Add (POST)
        [HttpPost("Add")]
        public async Task<IActionResult> Create(Student s)
        {
            DateTime minDate = new DateTime(1963, 1, 1);
            DateTime maxDate = new DateTime(2005, 12, 31);

            if (s.DateOfBorth < minDate || s.DateOfBorth > maxDate)
            {
                ModelState.AddModelError("DateOfBorth", "Ngày sinh phải nằm trong khoảng từ 01/01/1963 đến 12/31/2005.");
            }

            if (ModelState.IsValid)
            {
                if (s.AvatarFile != null && s.AvatarFile.Length > 0)
                {
                    var fileName = Path.GetFileName(s.AvatarFile.FileName);
                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + fileName;
                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");

                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await s.AvatarFile.CopyToAsync(fileStream);
                    }

                    s.AvatarUrl = uniqueFileName;
                }

                s.Id = listStudents.Any() ? listStudents.Last().Id + 1 : 101;
                listStudents.Add(s);

                return RedirectToAction("Index");
            }

            ViewBag.AllGenders = Enum.GetValues(typeof(Gender)).Cast<Gender>().ToList();
            ViewBag.AllBranches = new List<SelectListItem>()
            {
                new SelectListItem { Text = "IT", Value = "IT" },
                new SelectListItem { Text = "BE", Value = "BE" },
                new SelectListItem { Text = "CE", Value = "CE" },
                new SelectListItem { Text = "EE", Value = "EE" }
            };

            return View(s);
        }
    }
}