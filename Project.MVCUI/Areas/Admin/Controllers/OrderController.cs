using Project.BLL.GenericRepository.ConcRep;
using Project.ENTITIES.Models;
using Project.MVCUI.Areas.Admin.Data.AdminPageVMs.ListPageVMs;
using Project.MVCUI.AuthenticationClasses;
using Project.VM.PureVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.MVCUI.Areas.Admin.Controllers
{
    [AdminAuthentication]
    public class OrderController : Controller
    {
        OrderRepository _oRep;
        public OrderController()
        {
            _oRep = new OrderRepository();
        }
        // GET: Admin/Order
        public ActionResult ListOrders()
        {   
            ListAdminOrderPageVM lAO = new ListAdminOrderPageVM
            {
                AdminOrders = _oRep.Select(x => new AdminOrderVM
                {
                    ID = x.ID,
                    UserName = x.AppUser.UserName,
                    TotalPrice = x.TotalPrice,
                    ShippingAdress = x.ShippingAddress,
                    CreatedDate = x.CreatedDate,
                    ModifiedDate = x.ModifiedDate,
                    DeletedDate = x.DeletedDate,
                    Status = x.Status,

                }).ToList()
            };
            return View(lAO);
        }
        //public ActionResult AddOrder()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public ActionResult AddOrder(AdminOrderVM adminOrder)
        //{
        //    return RedirectToAction("ListOrders");

        //}
        public ActionResult ListAppUserOrders(string appUser)
        {
            ListAdminOrderPageVM lAO = new ListAdminOrderPageVM
            {
                AdminOrders = _oRep.Where(x =>x.AppUser.UserName == appUser).Select(x => new AdminOrderVM
                {
                    ID = x.ID,
                    UserName = x.AppUser.UserName,
                    TotalPrice = x.TotalPrice,
                    ShippingAdress = x.ShippingAddress,
                    CreatedDate = x.CreatedDate,
                    ModifiedDate = x.ModifiedDate,
                    DeletedDate = x.DeletedDate,
                    Status = x.Status,

                }).ToList()
            };
            return View(lAO);
        }
        public ActionResult UpdateOrder(int id)
        {
      
            return View();
        }
        [HttpPost]
        public ActionResult UpdateOrder(AdminOrderVM adminOrder)
        {
            Order toBeUpdated = _oRep.Find(adminOrder.ID);
            _oRep.Update(toBeUpdated);
            return RedirectToAction("ListOrders");
        }
        public ActionResult DeleteOrder(int id)
        {
            _oRep.Delete(_oRep.Find(id));
            return RedirectToAction("ListOrders");
        }
    }
}