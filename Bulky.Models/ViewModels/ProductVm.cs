using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bulky.Models.ViewModels
{
    public class ProductVm
    {
        public Products Product { get; set; }

        [ValidateNever] // So it doesn't try to bind it from the form post
        public IEnumerable<SelectListItem> CategoryList { get; set; }
    }

}
