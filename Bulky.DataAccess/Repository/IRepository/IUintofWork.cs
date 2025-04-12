using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using static Azure.Core.HttpHeader;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Azure;
using static System.Net.Mime.MediaTypeNames;
using System.Diagnostics.Metrics;
using System.Runtime.ConstrainedExecution;
using System.Net.NetworkInformation;

namespace Bulky.DataAccess.Repository.IRepository
{
   public interface IUintofWork 
    {
        ICategoryRepository Category { get; }
        IProductsRepository Products { get; }

//   ICategoryRepository is inside IUnitOfWork → So we can manage multiple repositories through a single interface.
//✔ We register IUnitOfWork in Program.cs using AddScoped → So Dependency Injection provides an instance when needed.
//✔ Using IUnitOfWork makes the code cleaner, more maintainable, and follows best practices.





        void save();// save method is save for all components or classes like products,orders,category etc.,  instead of writting every time
                    //we  put that in unit of work ,update is for some classes is same and for some its different so it should be in that particular repository
                    //        //Do NOT put Save() in IRepository<T>
                    //✅ Keep Save() in IUnitOfWork for proper transaction control

        //By following this structure, you ensure: ✔ Data consistency (all changes saved together)
        //✔ Better maintainability (repositories handle only data, Unit of Work handles transactions)
        //✔ Scalability(easier to extend with new repositories)



//        IRepository<T> is designed to be a generic data access layer.It provides methods for:

//Adding (Add())

//Removing(Remove(), RemoveRange())

//Retrieving(Get(), GetAll())

//These methods modify entities in memory(DbContext tracking), but they do not commit changes to the database.
//✅ Repositories should not be responsible for calling SaveChanges() because:

//If SaveChanges() is inside IRepository<T>, every Add() or Remove() call would immediately commit changes, breaking transaction control.

//Instead, UnitOfWork ensures all modifications across repositories are saved together in one transaction.




//        2. Save() in IRepository<T> Breaks Transaction Control
//Imagine a case where we need to add a Category and a Product together:

//Scenario 1: Save() Inside IRepository<T>(Bad Practice)
//csharp
//Copy
//Edit
//_categoryRepo.Add(newCategory);
//        _categoryRepo.Save();  // ❌ Changes committed immediately

//_productRepo.Add(newProduct);
//_productRepo.Save();  // ❌ Another separate commit
//🔴 Issue:

//Each repository commits changes separately.

//If _productRepo.Save() fails, the category is already saved, leading to data inconsistency.

//Scenario 2: Save() Inside IUnitOfWork(Best Practice)
//csharp
//Copy
//Edit
//_unitOfWork.Category.Add(newCategory);
//        _unitOfWork.Product.Add(newProduct);
//_unitOfWork.Save();  // ✅ Single transaction, both saved together
//✅ Why is this better?

//Both operations are committed together.

//If Product save fails, Category is also not saved, ensuring data consistency.
    }
}
