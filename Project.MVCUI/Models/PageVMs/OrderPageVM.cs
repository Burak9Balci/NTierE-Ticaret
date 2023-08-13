using Project.MVCUI.Areas.Admin.Data.AdminPageVMs.ListPageVMs;
using Project.MVCUI.OuterRequestTools;
using Project.VM.PureVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.MVCUI.Models.PageVMs
{
    public class OrderPageVM
    {
        public OrderVM Order { get; set; }
        public List<OrderVM> Orders { get; set; }
        public PaymentRequestModel PaymentRequestModel { get; set; }
    }
}