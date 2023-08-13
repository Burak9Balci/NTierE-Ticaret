using PagedList;
using Project.BLL.GenericRepository.ConcRep;
using Project.COMMON.Tools;
using Project.ENTITIES.Enums;
using Project.ENTITIES.Models;
using Project.MVCUI.AuthenticationClasses;
using Project.MVCUI.Models.PageVMs;
using Project.MVCUI.Models.ShoppingTools;
using Project.VM.PureVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Project.MVCUI.Controllers
{
    [MemberAuthentication]
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
        public ActionResult ConfirmOrder()
        {
            AppUser curentUser;
            if (Session["member"] != null)
            {
                curentUser = Session["member"] as AppUser;
            }
            return View();
        }
        //http://localhost:58604/api/Payment/RecivePayment
        [HttpPost]
        public ActionResult ConfirmOrder(OrderPageVM orderPageVM)
        {
            bool sonuc;
            Cart sepet = Session["scart"] as Cart;
            orderPageVM.PaymentRequestModel.ShoppingPrice = sepet.TotalPrice;

            using (HttpClient client = new HttpClient()) // HttpClient biziz
            {
                client.BaseAddress = new Uri("http://localhost:58604/api/");
                Task<HttpResponseMessage> postTask = client.PostAsJsonAsync("Payment/RecivePayment",orderPageVM.PaymentRequestModel);
                HttpResponseMessage result;
                try
                {
                    result = postTask.Result;
                }
                catch (Exception ex)
                {
                    TempData["baglantiRed"] = "Banka Baglantiyi reddetti";
                    return RedirectToAction("ShoppingList");
                }
                if (result.IsSuccessStatusCode) sonuc = true;
                else sonuc = false;

                if (sonuc)
                {
                    AppUser kullanici = Session["member"] as AppUser;
                    // orderPageVM.Order.UserID = kullanici.ID;
                    Order o = new Order
                    {
                        AppUserID = kullanici.ID,
                        TotalPrice = sepet.TotalPrice,
                        ShippingAddress = orderPageVM.Order.ShippingAdress
                    };
                    _oRep.Add(o);
                    foreach (CartItem item in sepet.Sepetim)
                    {
                        OrderDetail od = new OrderDetail();
                        od.OrderID = o.ID;
                        od.ProductID = item.ID;
                        od.TotalPrice = item.SubTotal;
                        od.Quantity = item.Amount;
                        _odRep.Add(od);

                        Product stoktanDusurulecek = _pRep.Find(item.ID);
                        stoktanDusurulecek.UnitsInStock -= item.Amount;
                        _pRep.Update(stoktanDusurulecek);
                    }
                    TempData["odeme"] = "Siparişiniz bize ulasmıstır...Tesekkür ederiz";

                   MailService.Send(o.AppUser.Email, body: $"Siparişiniz basarıyla alındı{orderPageVM.Order.TotalPrice}"); //Kullanıcıya aldıgı ürünleri de Mail yoluyla gönderin...
                    

                    Session.Remove("scart");
                    return RedirectToAction("ShoppingList");
                }
                else
                {
                    Task<string> s = result.Content.ReadAsStringAsync();
                    TempData["sorun"] = s;
                    return RedirectToAction("ShoppingList");
                }
            }
               
        }


        

    }
}