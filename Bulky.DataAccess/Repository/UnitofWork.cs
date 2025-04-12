using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository
{
    public class UnitofWork : IUintofWork
    {
        private readonly ApplicationDbcontext _db;

        public ICategoryRepository Category { get; private set; }

        public IProductsRepository Products  { get; private set;}

        public UnitofWork(ApplicationDbcontext db)
        {
            _db = db;
            Category = new CategoryRepository(_db);
            Products = new ProductsRepository(_db);
        }
        public void save()
        {
            _db.SaveChanges(); 
        }

       
    }
}
