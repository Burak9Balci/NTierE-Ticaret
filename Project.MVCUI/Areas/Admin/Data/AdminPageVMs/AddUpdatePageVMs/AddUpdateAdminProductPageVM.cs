using Project.VM.PureVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.MVCUI.Areas.Admin.Data.AdminPageVMs.AddUpdatePageVMs
{
    public class AddUpdateAdminProductPageVM
    {
        public AdminProductVM AdminProduct { get; set; }
        public List<AdminCategoryVM> AdminCategories { get; set; }
        public List<AdminSupplierVM> AdminSuppliers { get; set; }

    }
}
