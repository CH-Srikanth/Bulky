using Bulky.DataAccess.Repository;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Bulky.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;

namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private readonly IUintofWork _uintofWork;
        private readonly IWebHostEnvironment _hostEnvironment; // this is for uploading images
        public ProductsController(IUintofWork db, IWebHostEnvironment hostEnvironment)
        {
            _uintofWork = db;
            _hostEnvironment = hostEnvironment;
        }
        public IActionResult Index()
        {

            List<Products> objectcat = _uintofWork.Products.GetAll(includeProperties: "Category").ToList();
            //IncludeProperities always a case sensitive so, be careful while using it ,
            //instead of writting Category you wrote category then it will not work
            //if you want to include multiple properties then use this method

            //includeProperties: "Category" if you add this in create and edit  below error will show
            //'Microsoft.EntityFrameworkCore.Query.InvalidIncludePathError': Unable to find navigation 'Category' specified in string based include path 
            //    'Category'.This exception can be suppressed or logged by passing event ID 'CoreEventId.InvalidIncludePathError' to the 'ConfigureWarnings' 
            //method in 'DbContext.OnConfiguring' or 'AddDbContext'.


            return View(objectcat);
        }

        [HttpGet]
        public IActionResult Upsert(int? id)
        {
            // ViewBag.CatagoryList = CatagoryList;
            //ViewData["CatagoryList"] = CatagoryList;
            
            ProductVm productvm = new()
            {
                CategoryList = _uintofWork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                Product = new Products()
            };
            if(id!=null)
            {
                productvm.Product = _uintofWork.Products.Get(u => u.Id == id);
            }

            
            return View(productvm);
        }


        [HttpPost]
        public IActionResult Upsert(ProductVm obj,IFormFile? file)
        {


            //create post
            if (ModelState.IsValid)
            {
                if (file != null && file.Length > 0)
                {
                    string wwwRootPath = _hostEnvironment.WebRootPath;//this gives the path of wwwroot folder
                    if (wwwRootPath != null)

                    {
                        string fileName = Guid.NewGuid().ToString();//here iam generating a unique name for the image
                        string productpath = Path.Combine(wwwRootPath, "images", "products");//this is the full path of the image
                        string extension = Path.GetExtension(file.FileName);//this is the extension of the image
                        if (!Directory.Exists(productpath))
                        {
                            Directory.CreateDirectory(productpath); // ✅ Creates it if missing
                        }
                        if (!string.IsNullOrEmpty(obj.Product.ImageUrl))
                        {
                            var oldImagePath = Path.Combine(wwwRootPath, obj.Product.ImageUrl.TrimStart('\\'));
                            if (System.IO.File.Exists(oldImagePath))
                            {
                                System.IO.File.Delete(oldImagePath);
                            }
                        }

                        using (var fileStream = new FileStream(Path.Combine(productpath, fileName + extension), FileMode.Create))
                        {
                            file.CopyTo(fileStream);//this will copy the image to the path
                        }
                        //obj.Product.ImageUrl = @"\images\products\" + fileName + extension;//this is the path of the image
                        if (file != null)
                        {
                            // existing image delete + upload new image code...
                            obj.Product.ImageUrl = @"\images\products\" + fileName + extension;
                        }
                    }
                    else
                    {
                        // 🛠️ Preserve old image path if no new image is uploaded
                        var existingProduct = _uintofWork.Products.Get(u => u.Id == obj.Product.Id);
                        if (existingProduct != null)
                        {
                            obj.Product.ImageUrl = existingProduct.ImageUrl;
                        }
                    }



                }
                if (obj.Product.Id == 0)
                {
                    _uintofWork.Products.Add(obj.Product);
                }
                else
                {
                    _uintofWork.Products.Update(obj.Product);
                }
                _uintofWork.save();
                
                TempData["success"] = "Creation completed successfully!";
                return RedirectToAction("Index");

                
            }
          

           

            obj.CategoryList = _uintofWork.Category.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
            return View(obj); // Return the form if validation fails
            // RedirectToAction("Index", "Products");
        }




        // to use this youu nneed to delete.cshtml in views/products folder
        //[HttpGet]
        //public IActionResult Delete(int? id)
        //{
        //    if (id == null || id == 0)
        //    {
        //        return NotFound();//here you can return to error page if you have one 
        //    }
        //    Products Productsbyid = _uintofWork.Products.Get(u => u.Id == id);//here you can use firstordefault() also
        //    if (id == null || id == 0)
        //    {
        //        return NotFound();//here you can return to error page if you have one 
        //    }
        //    return View(Productsbyid);
        //}


        //[HttpPost, ActionName("Delete")]
        //public IActionResult DeleteById(Products obj)//get delete name and post delete name should be different actionname should be added as same as get of delete name to post delete method
        //{
        //    Products deletebyid = _uintofWork.Products.Get(u => u.Id == obj.Id);
        //    if (deletebyid.Id == null || deletebyid.Id == 0)
        //    {
        //        return NotFound();
        //    }
        //    _uintofWork.Products.Remove(deletebyid);

        //    _uintofWork.save();

        //    TempData["success"] = "Deletion completed successfully!";
        //    return RedirectToAction("Index");




        //    // Return the form if validation fails
        //    // RedirectToAction("Index", "Products");
        //}e




        #region Apicalls
        //in mvc api calls are in built so, you can directly work with them below you can find and this is pagination api 
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Products> objectcat = _uintofWork.Products.GetAll(includeProperties: "Category").ToList();
            return Json(new { data = objectcat });

        }

        [HttpDelete]//for delete popup i have used this attribute
        public IActionResult Delete(int id)
        {
            Products deletebyid = _uintofWork.Products.Get(u => u.Id == id);

            if (deletebyid == null)
            {
                return Json(new { success = false, message = "Product not found." });
            }
            if (!string.IsNullOrEmpty(deletebyid.ImageUrl))
            {

                var oldImagePath = Path.Combine(_hostEnvironment.WebRootPath, deletebyid.ImageUrl.TrimStart('\\'));

                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
            }

            _uintofWork.Products.Remove(deletebyid);
            _uintofWork.save();
            return Json(new { success = true, message = "Product deleted successfully." });

        }
        #endregion

    }
}

