using Project.BLL.GenericRepository.ConcRep;
using Project.COMMON.Tools;
using Project.ENTITIES.Models;
using Project.VM.PureVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.MVCUI.Controllers
{
    public class RegisterController : Controller
    {
        // GET: Register
        AppUserRepository _aURep;
        AppUserProfileRepository _pRep;
        public RegisterController()
        {
            _aURep = new AppUserRepository();
            _pRep = new AppUserProfileRepository();
        }
        public ActionResult RegisterNow()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RegisterNow(AppUserVM appUser,AppUserProfileVM appUserProfile)
        {
            if (_aURep.Any(x =>x.Email == appUser.Email))
            {
                ViewBag.Message = "Girdiğiniz Email Sistemde Kayitli";
                return View();
            }
            else if (_aURep.Any(x => x.UserName == appUser.UserName))
            {
                ViewBag.ZatenVar = "Girdiğiniz Kullanıcı Sistemde Kayitli";
                return View();
            }
            AppUser domainAppUser = new AppUser
            {
                UserName = appUser.UserName,
                Password = DanteCrypto.CrypHeaven(appUser.PassWord),
                Email = appUser.Email
            };
            _aURep.Add(domainAppUser);
            string mailBody = "Tekbrikler Hesabınıx olusturuldu hesabinizi aktive etmek için http://localhost:55032/Register/Activation/" + domainAppUser.ActivationCode +    " linkine tıklaya bilirsiniz";
            MailService.Send(appUser.Email,body:mailBody,subject:"Hesap Aktivasyon");
            
                AppUserProfile domainProfile = new AppUserProfile
                {
                    ID = domainAppUser.ID,
                    FirstName = appUserProfile.FirstName,
                    LastName = appUserProfile.LastName,
                };
                _pRep.Add(domainProfile);
            
            
            return View("RegisterOK");


        }
        public ActionResult RegisterOK()
        {
            return View();
        }
        public ActionResult Activation(Guid id)
        {
            AppUser aktiEdilicek = _aURep.FirstOrDefault(x =>x.ActivationCode == id);
            if (aktiEdilicek != null)
            {
                aktiEdilicek.Active = true;
                _aURep.Update(aktiEdilicek);
                TempData["HesapAktifMi"] = "Hesap aktif";
                return RedirectToAction("Login","Home");
            }
            TempData["HesapAktifMi"] = "Hesap aktif degil";
            return RedirectToAction("Login", "Home");
        }
    }
}