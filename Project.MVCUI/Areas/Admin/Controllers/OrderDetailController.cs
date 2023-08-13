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
    public class OrderDetailController : Controller
    {
        // GET: Admin/Order
        OrderDetailRepository _oDRep;
        public OrderDetailController()
        {
            _oDRep = new OrderDetailRepository();
        }
        public ActionResult ListOrderDetails()
        {
            ListAdminOrderDetailPageVM lAO = new ListAdminOrderDetailPageVM
            {
                AdminOrderDetails = _oDRep.Select(x => new AdminOrderDetailVM
                {

                    UserName = x.Order.AppUser.UserName,
                    ProductName = x.Product == null ? "ProductName Girilmemiştir" : x.Product.ProductName,
                    Amount = x.Quantity,
                    UnitPrice = x.Product == null ? Convert.ToInt32("UnitPrice Girilmemiştir") : x.Product.UnitPrice,
                    TotalPrice = x.TotalPrice,
                    ImagePath = x.Product == null ? "Fotograf Girilmemiştir" : x.Product.ImagePath,
                    CategoryName = x.Product  == null ? "Category Girilmemiştir" : x.Product.Category.CategoryName,                    
                    SupplierCompanyName = x.Product == null ? "Supplier Girilmemiştir" : x.Product.Supplier.CompanyName,
                    CreatedDate = x.CreatedDate,
                    DeletedDate = x.DeletedDate,
                    ModifiedDate = x.ModifiedDate,
                    Status = x.Status,





                }).ToList(),
            };
            return View(lAO);
        }
        public ActionResult ListAppUserOrderDetails(int id)
        {
            ListAdminOrderDetailPageVM lAO = new ListAdminOrderDetailPageVM
            {
                AdminOrderDetails = _oDRep.Where(x =>x.Order.AppUser.ID == id).Select(x => new AdminOrderDetailVM
                {

                    UserName = x.Order.AppUser.UserName,
                    ProductName = x.Product == null ? "ProductName Girilmemiştir" : x.Product.ProductName,
                    Amount = x.Quantity,
                    UnitPrice = x.Product == null ? Convert.ToInt32("UnitPrice Girilmemiştir") : x.Product.UnitPrice,
                    TotalPrice = x.TotalPrice,
                    ImagePath = x.Product == null ? "Fotograf Girilmemiştir" : x.Product.ImagePath,
                    CategoryName = x.Product == null ? "Category Girilmemiştir" : x.Product.Category.CategoryName,
                    SupplierCompanyName = x.Product == null ? "Supplier Girilmemiştir" : x.Product.Supplier.CompanyName,
                    CreatedDate = x.CreatedDate,
                    DeletedDate = x.DeletedDate,
                    ModifiedDate = x.ModifiedDate,
                    Status = x.Status,





                }).ToList(),
            };
            return View(lAO);
        }
        public ActionResult UpdateOrderDetail(int id)
        {
            return View();
        }
        [HttpPost]
        public ActionResult UpdateOrderDetail(AdminOrderDetailVM adminOrderDetail)
        { 
            OrderDetail toBeUpdated = _oDRep.Find(adminOrderDetail.ID);
            _oDRep.Update(toBeUpdated);
            return RedirectToAction("ListOrderDetail");
        }
        public ActionResult DeleteOrderDetail(int id)
        {
            _oDRep.Delete(_oDRep.Find(id)); 
            return RedirectToAction("ListOrderDetail");
        }
    }
}