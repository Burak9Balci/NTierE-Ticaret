using Project.BLL.GenericRepository.ConcRep;
using Project.VM.PageVMs;
using Project.VM.PureVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.MVCUI.Areas.Admin.Controllers
{
    public class OrderController : Controller
    {
        // GET: Admin/Order
        OrderRepository _oRep;
        public OrderController()
        {
            _oRep = new OrderRepository();
        }
        public ActionResult ListOrders()
        {
            ListAdminOrderPageVM lAOPVM = new ListAdminOrderPageVM
            {
                AdminOrders = _oRep.Select(x => new AdminOrderVM
                {
                    ID = x.ID,
                    ShippingAdress = x.ShippingAddress,
                    TotalPrice = x.TotalPrice


                }).ToList(),
            };
            return View(lAOPVM);
        }
    }
}