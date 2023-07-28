using Project.VM.PureVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.MVCUI.Areas.Admin.Data.AdminPageVMs.AddUpdatePageVMs
{
    public class AddUpdateAdminOrderPageVM
    {
        public AdminOrderVM AdminOrder { get; set; }
        public List<AdminAppUserVM> AdminAppUser { get; set; }
    }
}