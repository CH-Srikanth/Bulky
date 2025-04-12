using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUintofWork _uintofWork;
        public CategoryController(IUintofWork db)
        {
            _uintofWork = db;
        }
        public IActionResult Index()
        {

            List<Category> objectcat = _uintofWork.Category.GetAll().ToList();
            return View(objectcat);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (ModelState.IsValid)
            {
               _uintofWork.Category.Add(obj);
                _uintofWork.save();
                TempData["success"] = "Creation completed successfully!";
                return RedirectToAction("Index");


            }

            return View(obj); // Return the form if validation fails
            // RedirectToAction("Index", "Category");
        }




        [HttpGet]
        public IActionResult Edit(int?id)
        {
            if(id == null || id == 0)
            {
                return NotFound();//here you can return to error page if you have one 
            }
            Category categorybyid =_uintofWork.Category.Get(u=>u.Id==id);
            if(id==null || id == 0)
            {
                return NotFound();//here you can return to error page if you have one 
            }
            return View(categorybyid);
        }


        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
               _uintofWork.Category.Update(obj);
               _uintofWork.save();
                TempData["success"] = "Updated completed successfully!";
                return RedirectToAction("Index");


            }

            return View(obj); // Return the form if validation fails
            // RedirectToAction("Index", "Category");
        }


        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();//here you can return to error page if you have one 
            }
            Category categorybyid =_uintofWork.Category.Get(u=>u.Id==id);//here you can use firstordefault() also
            if (id == null || id == 0)
            {
                return NotFound();//here you can return to error page if you have one 
            }
            return View(categorybyid);
        }
 

        [HttpPost ,ActionName("Delete")]
        public IActionResult DeleteById(Category obj)//get delete name and post delete name should be different actionname should be added as same as get of delete name to post delete method
        {
            Category deletebyid =_uintofWork.Category.Get(u=>u.Id==obj.Id);
            if(deletebyid.Id==null || deletebyid.Id == 0)
            {
                return NotFound();
            }
           _uintofWork.Category.Remove(deletebyid);
           
           _uintofWork.save();

            TempData["success"] = "Deletion completed successfully!";
            return RedirectToAction("Index");




            // Return the form if validation fails
            // RedirectToAction("Index", "Category");
        }

    }
}
