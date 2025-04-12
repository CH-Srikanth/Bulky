using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository.IRepository
{
    //<T> this is the generic declaration  of any type of class ex:category.cs,order.cs, product.cs and many more
     public interface IRepository<T> where T:class
    {
        IEnumerable<T> GetAll(string? includeProperties = null);
        T Get(Expression<Func<T, bool>> filter, string? includeProperties = null); //When we  have to use link statemets  use this expression for generic declartions  ex:category categorybyid = _db.Category.Find(id); in get method on edit  in category controller

        void Add(T entity);
//here we didnt add update  because different classes require to update certain properties and some classes need to be Updated all properities so, let them update in controller 
        void Remove(T entity);
        void RemoveRange(IEnumerable<T>entity);



    }
}
