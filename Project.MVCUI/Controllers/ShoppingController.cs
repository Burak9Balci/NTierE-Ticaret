using PagedList;
using Project.BLL.GenericRepository.ConcRep;
using Project.ENTITIES.Enums;
using Project.ENTITIES.Models;
using Project.MVCUI.Models.PageVMs;
using Project.MVCUI.Models.ShoppingTools;
using Project.VM.PureVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.MVCUI.Controllers
{
    public class ShoppingController : Controller
    {
        ProductRepository _pRep;
        CategoryRepository _cRep;
        OrderDetailRepository _odRep;
        OrderRepository _oRep;
        // GET: Shopping
        public ShoppingController()
        {
            _odRep = new OrderDetailRepository();
            _cRep = new CategoryRepository();
            _pRep = new ProductRepository();
            _oRep = new OrderRepository();
        }
        public ActionResult ShoppingList(int? page,int? categoryID)
        {
            PaginationVM pav = new PaginationVM
            {
               PageProducts = categoryID == null ? GetActivProducts().ToPagedList(page ?? 1, 9) : GetActivProducts().Where(x => x.CategoryID == categoryID).ToPagedList(page ?? 1, 9),
               Categories = GetActivCategories()
            };
            if (categoryID != null) TempData["catID"] = categoryID;
            
            return View(pav);
        }
        private List<ProductVM> GetActivProducts()
        {
            List<ProductVM> actives = _pRep.Where(x =>x.Status != DataStatus.Deleted).Select(x => new ProductVM
            {
                ID = x.ID,
                ProductName = x.ProductName,
                UnitPrice = x.UnitPrice,
                CategoryName = x.Category.CategoryName,
                CategoryID = x.Category.ID,
                Status = x.Status,

            }).ToList();
            return actives;
        }
        private List<CategoryVM> GetActivCategories()
        {
            List<CategoryVM> actives = _cRep.Where(x =>x.Status != DataStatus.Deleted).Select(x => new CategoryVM
            {
                ID = x.ID,
                CategoryName = x.CategoryName,
                Description = x.Description,
                Status = x.Status

            }).ToList();
            return actives;
        }
        public ActionResult CartPage()
        {
            if (Session["scart"] != null)
            {
                CartPageVM cart = new CartPageVM
                {
                    Cart = Session["scart"] as Cart
                };
                return View(cart);
            }
            TempData["bos"] = "Sepetiniz Bos";
            return RedirectToAction("ShoppingList");
        }
        public ActionResult AddToCart(int id)
        {
            Cart c = Session["scart"] != null ? Session["scart"] as Cart : new Cart();
            Product p = _pRep.Find(id);
            CartItem item = new CartItem
            {
                ID = p.ID,
                ProductName = p.ProductName,
                Price = p.UnitPrice,
                ImagePath = p.ImagePath
            };
            c.SepeteEkle(item);
            Session["scart"] = c;
            return RedirectToAction("ShoppingList");
        }
        public ActionResult DeleteFromCart(int id)
        {
            if (Session["scart"] != null)
            {
               Cart c = Session["scart"] as Cart;
                c.SepettenCikar(id);
                if (c.Sepetim.Count == 0)
                {
                    Session.Remove("scart");
                    TempData["sepetBos"] = "Sepetinizdeki tüm ürünler cıkarılmıstır";
                    return RedirectToAction("ShoppingList");
                }
            }
            return RedirectToAction("CartPage");
        }

    }
}