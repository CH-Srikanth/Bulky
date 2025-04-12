using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository
{
    public class ProductsRepository : Repository<Products>, IProductsRepository
    {
        private readonly ApplicationDbcontext _db;
        public ProductsRepository(ApplicationDbcontext db):base(db)
        {

            _db = db;
        }

        public void Update(Products obj)
        {
            var objfromdb = _db.Products.FirstOrDefault(u => u.Id == obj.Id);
            if(objfromdb!=null)
            {
                objfromdb.Title = obj.Title;
                objfromdb.ISBN = obj.ISBN;
                objfromdb.Author = obj.Author;
                objfromdb.CategoryId = obj.CategoryId;
                objfromdb.ListPrice = obj.ListPrice;
                objfromdb.Price50 = obj.Price50;
                objfromdb.Price100 = obj.Price100;
                if(objfromdb.ImageUrl!=null)
                {
                    objfromdb.ImageUrl = obj.ImageUrl;
                }

            }

        }
    }
}
