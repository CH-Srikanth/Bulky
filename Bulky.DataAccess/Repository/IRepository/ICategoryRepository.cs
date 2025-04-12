using Bulky.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository.IRepository
{
  public   interface ICategoryRepository:IRepository<Category>
    {
        //Here we take the methods which we  missed in IRepositatory
        void Update(Category obj );
        

    }
}
