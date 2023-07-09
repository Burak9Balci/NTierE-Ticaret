using Project.BLL.GenericRepository.ConcRep;
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
    public class CategoryController : Controller
    {
        // GET: Admin/Category
        CategoryRepository _cRep;
        public CategoryController()
        {
            _cRep = new CategoryRepository();
        }
        public ActionResult ListCategories()
        {
            ListAdminCategoryPageVM lCVM = new ListAdminCategoryPageVM
            {
                AdminCategories = _cRep.Select(x => new AdminCategoryVM
                {
                    ID = x.ID,
                    CategoryName = x.CategoryName,
                    Description = x.Description


                }).ToList()
            };
            return View(lCVM);
        }
        public ActionResult AddCategory()
        {
            return View();
        }
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