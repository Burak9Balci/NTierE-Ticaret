using Project.BLL.GenericRepository.ConcRep;
using Project.COMMON.Tools;
using Project.ENTITIES.Enums;
using Project.ENTITIES.Models;
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
            

            AppUser aa = _aURep.FirstOrDefault(x =>x.Email == appUser.Email);
            DanteCrypto.CrypHell(aa.Password);
            appUser.PassWord = aa.Password; 
            if (_aURep.Any(x => x.Email == appUser.Email))
            {
              
              
               if (_aURep.Any(x =>x.Role == UserRole.Admin && x.Password == appUser.PassWord))
               {
                    Session["admin"] = _aURep.FirstOrDefault(x => x.Email == appUser.Email && x.Password == appUser.PassWord);
                    return Redirect("/Admin/AppUser/ListAppUser");
               }
               else if (_aURep.Any(x =>x.Role == UserRole.Member && x.Password == appUser.PassWord))
               {
                    Session["member"] = _aURep.FirstOrDefault(x => x.Email == appUser.Email && x.Password == appUser.PassWord);
                    return RedirectToAction("ShoppingList","Shopping");
               }

            }
            TempData["kullaniciyok"] = "Böyle bir Kullanici yok";
            return View();
        }

        
    }
}