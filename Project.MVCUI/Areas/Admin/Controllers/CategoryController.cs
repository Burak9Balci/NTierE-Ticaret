using Project.BLL.GenericRepository.ConcRep;
using Project.ENTITIES.Models;
using Project.MVCUI.Areas.Admin.Data.AdminPageVMs.AddUpdatePageVMs;
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
    public class CategoryController : Controller
    {
        // GET: Admin/Category
        CategoryRepository _cRep;
        public CategoryController()
        {
            _cRep = new CategoryRepository();
        }
        public ActionResult ListCategories(int? id)
        {
            List<AdminCategoryVM> adminCategories;
            if (id == null)
            {
                adminCategories = _cRep.Select(x => new AdminCategoryVM
                {
                    ID = x.ID,
                    CategoryName = x.CategoryName,
                    Description = x.Description,
                    CreatedDate = x.CreatedDate,
                    DeletedDate = x.DeletedDate,
                    ModifiedDate = x.ModifiedDate,
                    Status = x.Status,

                }).ToList();
            }
            else
            {
                adminCategories = _cRep.Where(x =>x.ID == id).Select(x => new AdminCategoryVM
                {
                    ID = x.ID,
                    CategoryName = x.CategoryName,
                    Description = x.Description,
                    CreatedDate = x.CreatedDate,
                    DeletedDate = x.DeletedDate,
                    ModifiedDate = x.ModifiedDate,
                    Status = x.Status,

                }).ToList();
            }
            ListAdminCategoryPageVM lAC = new ListAdminCategoryPageVM
            {
                AdminCategories = adminCategories
            };
            return View(lAC);
        }
        public ActionResult AddCategory()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddCategory(AdminCategoryVM adminCategory)
        {
            Category c = new Category
            {
                CategoryName = adminCategory.CategoryName,
                Description = adminCategory.Description

            };
            _cRep.Add(c);
            return RedirectToAction("ListCategories");
        }
        public ActionResult UpdateCategory(int id) 
        {
            AddUpdateAdminCategoryPageVM aUAC = new AddUpdateAdminCategoryPageVM
            {
                AdminCategory = _cRep.Where(x =>x.ID == id).Select(x => new AdminCategoryVM
                {
                    ID = id,
                    CategoryName = x.CategoryName,
                    Description = x.Description

                }).FirstOrDefault(),
            };
            return View(aUAC);
        }
        [HttpPost]
        public ActionResult UpdateCategory(AdminCategoryVM adminCategory)
        {
            Category c = _cRep.Find(adminCategory.ID);
            _cRep.Update(c);
            return RedirectToAction("ListCategories");
        }
        public ActionResult DeleteCategory(int id)
        {
            _cRep.Delete(_cRep.Find(id));
            return RedirectToAction("ListCategories");
        }
    }
}