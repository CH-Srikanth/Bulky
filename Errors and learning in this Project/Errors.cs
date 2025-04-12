using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace Errors_and_learning_in_this_Project
{
    class Errors
    {

        //        Errors

        /* 1.Unable to create a 'DbContext' of type ''. The exception 'The entity type 'SelectListItem' 
                requires a primary key to be defined.If you intended to use a keyless entity type, call 
                'HasNoKey' in 'OnModelCreating'.For more information on keyless entity types, see https://go.microsoft.com/fwlink/?linkid=2141943.' 

                fix : dotnet clean
                      dotnet build

                i added the line   modelBuilder.Ignore<SelectListItem>(); // ✅ Tells EF to ignore it
                   base.OnModelCreating(modelBuilder); in the application dbcontext class               */



       /*2.includeProperties: "Category" if you add this in create and edit  below error will show
        'Microsoft.EntityFrameworkCore.Query.InvalidIncludePathError': Unable to find navigation 'Category' specified in string based include path 
            'Category'.This exception can be suppressed or logged by passing event ID 'CoreEventId.InvalidIncludePathError' to the 'ConfigureWarnings' 
        method in 'DbContext.OnConfiguring' or 'AddDbContext'.*/



    }
}
