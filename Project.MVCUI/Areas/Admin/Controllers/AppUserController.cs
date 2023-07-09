using Project.BLL.GenericRepository.ConcRep;
using Project.COMMON.Tools;
using Project.ENTITIES.Models;
using Project.VM.PageVMs;
using Project.VM.PureVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.MVCUI.Areas.Admin.Controllers
{
    public class AppUserController : Controller
    {
        // GET: Admin/AppUser
        AppUserRepository _aURep;
        public AppUserController()
        {
            _aURep = new AppUserRepository();
        }
        public ActionResult ListAppUser()
        {
            ListAdminAppUserPageVM aUVM = new ListAdminAppUserPageVM
            {
                AdminAppUsers = _aURep.Select(x => new AdminAppUserVM
                {
                    ID = x.ID,
                    UserName = x.UserName,
                    PassWord = x.Password,
                    Status = x.Status,

                }).ToList(),
            };
            return View(aUVM);
        }
        public ActionResult AddAppUser()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddAppUser(AdminAppUserVM appUser)
        {
            AppUser aU = new AppUser
            {
                UserName = appUser.UserName,
                Password = DantexCrypt.Crypt(appUser.PassWord)
            };
            _aURep.Add(aU);
            return RedirectToAction("ListAppUser");
        }
        public ActionResult UpdateAppUser(int id)
        {
            AddUpdateAdminAppUserPageVM aU = new AddUpdateAdminAppUserPageVM
            {
                AdminAppUser = _aURep.Where(x =>x.ID == id).Select(x =>new AdminAppUserVM
                {
                    ID = id,
                    UserName = x.UserName,
                    PassWord = x.Password,
                    Status = x.Status,

                }).FirstOrDefault(),
            };
            return View(aU);
        }
        public ActionResult UpdateAppUser(AdminAppUserVM appUser)
        {
            AppUser a = _aURep.Find(appUser.ID);
            _aURep.Update(a);
            return RedirectToAction("ListAppUser");
        }
        public ActionResult DeleteAppUser(int id)
        {
            _aURep.Delete(_aURep.Find(id));
            return RedirectToAction("ListAppUser");
        }
    }
}