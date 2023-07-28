using Project.BLL.GenericRepository.ConcRep;
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
    public class ProductController : Controller
    {
        ProductRepository _pRep;
        CategoryRepository _cRep;
        SupplierRepository _sRep;
        // GET: Admin/Product ctor      
        public ProductController()
        {
            _pRep = new ProductRepository();
            _cRep = new CategoryRepository();
            _sRep = new SupplierRepository();
        }
        public ActionResult ListProduct(int? categoryID)
        {
            List<AdminProductVM> products;
            if (categoryID == null)
            {
                products = _pRep.Select(x => new AdminProductVM
                {
                    ID = x.ID,
                    ProductName = x.ProductName,
                    CategoryName = x.Category == null ? "Category Girilmemiştir" : x.Category.CategoryName,
                    UnitPrice = x.UnitPrice,
                    UnitInStock = x.UnitsInStock,
                    ImagePath = x.ImagePath,
                    CreatedDate = x.CreatedDate,
                    DeletedDate = x.DeletedDate,
                    ModifiedDate = x.ModifiedDate,
                    Status = x.Status,
                    SupplierName = x.Supplier == null ? "Supplier Girilmemiştir" : x.Supplier.CompanyName,


                }).ToList();
            }
            else
            {
                products = _pRep.Where(x =>x.CategoryID == categoryID).Select(x => new AdminProductVM
                {
                    ID = x.ID,
                    ProductName = x.ProductName,
                    CategoryName = x.Category == null ? "Category Girilmemiştir" : x.Category.CategoryName,
                    UnitPrice = x.UnitPrice,
                    UnitInStock = x.UnitsInStock,
                    ImagePath = x.ImagePath,
                    CreatedDate = x.CreatedDate,
                    DeletedDate = x.DeletedDate,
                    ModifiedDate = x.ModifiedDate,
                    Status = x.Status,
                    SupplierName = x.Supplier == null ? "Supplier Girilmemiştir" : x.Supplier.CompanyName,


                }).ToList();
            }
            ListAdminProductPageVM lAP = new ListAdminProductPageVM
            {
               AdminProducts = products
            };
            return View(lAP);
        }
        public ActionResult AddProduct() 
        {
            AddUpdateAdminProductPageVM aUAP = new AddUpdateAdminProductPageVM
            {
                AdminCategories = _cRep.Select(x => new AdminCategoryVM
                {
                    ID = x.ID,
                    CategoryName = x.CategoryName

                }).ToList(),
                AdminSuppliers = _sRep.Select(x => new AdminSupplierVM
                {
                    ID= x.ID,
                    CompanyName = x.CompanyName

                }).ToList()
                
            };
            return View(aUAP);
        }
        [HttpPost]
        public ActionResult AddProduct(AdminProductVM adminProduct)
        {
            Product p = new Product
            {
                ProductName = adminProduct.ProductName,
                UnitPrice =adminProduct.UnitPrice,
                Status = adminProduct.Status,
                CategoryID = Convert.ToInt32(adminProduct.CategoryName)
            };
            _pRep.Add(p);
            return RedirectToAction("ListProduct");
        }
        public ActionResult UpdateProduct(int id)
        {
            AddUpdateAdminProductPageVM aUA = new AddUpdateAdminProductPageVM
            {
                AdminProduct = _pRep.Where(x =>x.ID == id).Select(x => new AdminProductVM
                {
                    ID = id,
                    ProductName = x.ProductName,
                    UnitPrice = x.UnitPrice,
                    UnitInStock = x.UnitsInStock,
                    CategoryName = x.Category == null ? "Category Girilmemiştir" : x.CategoryID.ToString(),
                    SupplierName = x.Supplier == null ? "Supplier Girilmemiştir" : x.SupplierID.ToString(),

                }).FirstOrDefault(),
                AdminCategories = _cRep.Select(x => new AdminCategoryVM
                {
                    ID = x.ID,
                    CategoryName = x.CategoryName

                }).ToList(),
                AdminSuppliers = _sRep.Select(x => new AdminSupplierVM
                {
                    ID = x.ID,
                    CompanyName = x.CompanyName


                }).ToList()
                
            };
            return View(aUA);
        }
        [HttpPost]
        public ActionResult UpdateProduct(AdminProductVM adminProduct)
        {
            Product p = _pRep.Find(adminProduct.ID);
            _pRep.Update(p);
            return RedirectToAction("ListProduct");
        }
        public ActionResult DeleteProduct(int id)
        {
            _pRep.Delete(_pRep.Find(id));
            return RedirectToAction("ListProduct");
        }
    }
}