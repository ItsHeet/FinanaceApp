using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using MyApp.Data;
using MyApp.Models;

namespace MyApp.Controllers
{
    public class StudentsController : Controller
    {
        private readonly MyAppContext dbConext;
        public StudentsController(MyAppContext dbConext) {
            this.dbConext = dbConext;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }



        [HttpPost]

        public async Task <IActionResult> Add(AddStudentViewModel viewModel) 
        {
            var student = new Student
            {
                Name = viewModel.Name,
                Email = viewModel.Email,
                Phone = viewModel.Phone,
                Subcribed = viewModel.Subcribed
            };

           await dbConext.Students.AddAsync(student);
            dbConext.SaveChanges();


            return RedirectToAction("List","Students");
        }


        [HttpGet]

        public async Task<IActionResult> List() {

            var students = await dbConext.Students.ToListAsync();

            return View(students);
            
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id) {
           var students= await dbConext.Students.FindAsync(id);
            return View(students);
        }

        [HttpPost]

        public async Task<IActionResult> Edit(Student viewModel) {
          var student=  await dbConext.Students.FindAsync(viewModel.Id);

            if (student is not null) {
                student.Name = viewModel.Name;
                student.Email = viewModel.Email;
                student.Phone = viewModel.Phone;
                student.Subcribed = viewModel.Subcribed;

                dbConext.SaveChangesAsync();
            }
            return RedirectToAction("List","Students");
        }


        [HttpPost]
        public async Task<IActionResult> Delete(Student viewModel) {
           var student= await dbConext.Students.FindAsync(viewModel.Id);

            if (student is not null) {

                dbConext.Students.Remove(student);
                await dbConext.SaveChangesAsync();
            }

            return RedirectToAction("List","Students");
        }
    }
}
