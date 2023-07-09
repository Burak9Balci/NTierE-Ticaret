using Project.BLL.GenericRepository.ConcRep;
using Project.ENTITIES.Enums;
using Project.VM.PureVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.MVCUI.Controllers
{
    public class HomeController : Controller
    {
        AppUserRepository _aURep;
        public HomeController()
        {
            _aURep = new AppUserRepository();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(AppUserVM appUser)
        {
            if (_aURep.Any(x =>x.UserName == appUser.UserName && x.Password == appUser.PassWord && x.Role == UserRole.Admin))
            {
                Session["admin"] = _aURep.FirstOrDefault(x => x.UserName == appUser.UserName && x.Password == appUser.PassWord);
                return RedirectToAction("ListAppUser");
                //
            }
            else if (_aURep.Any(x => x.UserName == appUser.UserName && x.Password == appUser.PassWord && x.Role == UserRole.Member))
            {
                Session["member"] = _aURep.FirstOrDefault(x => x.UserName == appUser.UserName && x.Password == appUser.PassWord);
                return RedirectToAction("ShoppingList");
            }
            TempData["kullanıcıyok"] = "Böyle bir Kullanıcı yok";
            return View();
        }

        
    }
}