using Project.BLL.GenericRepository.ConcRep;
using Project.COMMON.Tools;
using Project.ENTITIES.Models;
using Project.MVCUI.Areas.Admin.Data.AdminPageVMs.AddUpdatePageVMs;
using Project.MVCUI.Areas.Admin.Data.AdminPageVMs.ListPageVMs;
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
        public ActionResult ListAppUser(int? id)
        {
            List<AdminAppUserVM> adminApps;
            if (id == null)
            {
                adminApps = _aURep.Select(x => new AdminAppUserVM
                {
                  ID = x.ID,
                  UserName = x.UserName,
                  PassWord = x.Password,
                  Email = x.Email,
                  CreatedDate = x.CreatedDate,
                  DeletedDate = x.DeletedDate,
                  ModifiedDate = x.ModifiedDate,
                  Status = x.Status,

                }).ToList();

            }
            else
            {
                adminApps = _aURep.Where(x =>x.ID ==id).Select(x => new AdminAppUserVM
                {
                    ID = x.ID,
                    UserName = x.UserName,
                    PassWord = x.Password,
                    Email = x.Email,
                    CreatedDate = x.CreatedDate,
                    DeletedDate = x.DeletedDate,
                    ModifiedDate = x.ModifiedDate,
                    Status = x.Status,

                }).ToList();
            }
            
           
            ListAdminAppUserPageVM aUVM = new ListAdminAppUserPageVM
            {
                AdminAppUsers = adminApps
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
                Password = DanteCrypto.CrypHeaven(appUser.PassWord),
                Email = appUser.Email

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
                    Email = x.Email,
                    Status = x.Status,

                }).FirstOrDefault(),
            };
            return View(aU);
        }
        [HttpPost]
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