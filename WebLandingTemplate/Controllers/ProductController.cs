using System;
using PagedList;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using WebLandingTemplate.Models;
using WebLandingTemplateBusinessLogic.Interface;
using WebLandingTemplateBusinessLogic.Logic;
using WebLandingTemplateDomainModel.Models;
using WebLandingTemplateDomainModel.Enums;


namespace WebLandingTemplate.Controllers
{
    public class ProductController : Controller
    {
        IProductBusiness _productBusiness;
        ISupplierBusiness _supplierBusiness;
        ICategoryBusiness _categoryBusiness;


        public ProductController(ProductBusiness productBusiness, SupplierBusiness supplierBusiness, CategoryBusiness categoryBusiness)
        {
            _productBusiness = productBusiness;
            _supplierBusiness = supplierBusiness;
            _categoryBusiness = categoryBusiness;
        }
        


        private IEnumerable<SelectListItem> DataItems(int pageSize)
        {
            const int valueA = 3, valueB = 6, valueC = 9, valueD = 15;
            List<SelectListItem> list = new List<SelectListItem>();
            //list.Add(new SelectListItem() { Value = null, Text = "---Select---" });
            list.Add(new SelectListItem() { Value = valueA.ToString(), Text = valueA.ToString(), Selected = false });
            list.Add(new SelectListItem() { Value = valueB.ToString(), Text = valueB.ToString(), Selected = false });
            list.Add(new SelectListItem() { Value = valueC.ToString(), Text = valueC.ToString(), Selected = false });
            list.Add(new SelectListItem() { Value = valueD.ToString(), Text = valueD.ToString(), Selected = false });

            switch (pageSize)
            {
                case valueA:
                    list.ElementAtOrDefault(0).Selected = true;
                    break;
                case 6:
                    list.ElementAtOrDefault(1).Selected = true;
                    break;
                case 9:
                    list.ElementAtOrDefault(2).Selected = true;
                    break;
                case 15:
                    list.ElementAtOrDefault(3).Selected = true;
                    break;
                default:
                    break;
            }

            return new SelectList(list, "Value", "Text", "Selected");
        }

        private IEnumerable<SelectListItem> DataFilterSupplier()
        {
            //var istenum = Enum.GetValues(typeof(ItemCodeTypeEnum)).Cast<ItemCodeTypeEnum>().Select(p => new SelectListItem()
            var istenum = _supplierBusiness.GetAllSupplier().Select(p => new SelectListItem()

                        {
                            Text = p.Name,
                Value = ((int)p.ProveedorId).ToString()
            }).ToList();
            //istenum.Insert(0, new SelectListItem() { Value = null, Text = "--Seleciona--", Selected = true });
           
            return new SelectList(istenum, "Value", "Text", "Selected");
        }

        private IEnumerable<SelectListItem> DataFilterCategory()
        {
            //var istenum = Enum.GetValues(typeof(ItemCodeTypeEnum)).Cast<ItemCodeTypeEnum>().Select(p => new SelectListItem()
            var istenum = _categoryBusiness.GetAllCategory().Where(p=>p.ItemCodeType==1).Select(p => new SelectListItem()

            {
                Text = p.Name,
                Value = ((int)p.CategoryId).ToString()
            }).ToList();
            //istenum.Insert(0, new SelectListItem() { Value = null, Text = "--Seleciona--", Selected = true });

            return new SelectList(istenum, "Value", "Text", "Selected");
        }

        private IEnumerable<SelectListItem> DataFilterCategory(int? idSelected)
        {
            //var istenum = Enum.GetValues(typeof(ItemCodeTypeEnum)).Cast<ItemCodeTypeEnum>().Select(p => new SelectListItem()
            var istenum = _categoryBusiness.GetAllCategory().Where(p => p.ItemCodeType == 1).Select(p => new SelectListItem()

            {
                Text = p.Name,
                Value = ((int)p.CategoryId).ToString()
            }).ToList();
            istenum.Insert(0, new SelectListItem() { Value = null, Text = "--Seleciona--", Selected = true });

            return new SelectList(istenum, "Value", "Text", "Selected");
        }

        private IEnumerable<SelectListItem> DataFilterSupplierEdit(int? idSelected)
        {
            //var istenum = Enum.GetValues(typeof(ItemCodeTypeEnum)).Cast<ItemCodeTypeEnum>().Select(p => new SelectListItem()
            var istenum = _supplierBusiness.GetAllSupplier().Select(p => new SelectListItem()

            {
                Text = p.Name,
                Value = ((int)p.ProveedorId).ToString()
               
            }).ToList();


              

            var a = new SelectList(istenum, "Value", "Text", "Selected");
            
            return a;
        }

        // GET: Product
        [Authorize]
        public ActionResult Index(int? page, string searchString, int pageSize = 3)
        {

            ViewBag.dropdownsrc = DataItems(pageSize);
            ViewBag.typeCategory = DataFilterCategory();

            int pageNumber = (page ?? 1);
            var listaVM = new List<ProductVM>();

            if (!string.IsNullOrEmpty(searchString))
            {
                var listaDto = _productBusiness.GetAllProducts().Where(c => c.ProductName.Contains(searchString) || c.Description.Contains(searchString));
                AutoMapper.Mapper.Map(listaDto, listaVM);
            }
            else
            {
                var listaDto = _productBusiness.GetAllProducts();
                AutoMapper.Mapper.Map(listaDto, listaVM);
                if (listaVM != null)
                {
                    foreach (var item in listaVM)
                    {
                        int count = ViewBag.typeCategory.Items.Count;
                        for (int i = 0; i < count ; i++)
                        {
                            if (item.ProductType == Int32.Parse(ViewBag.typeCategory.Items[i].Value))
                            {
                                item.CategoryName = ViewBag.typeCategory.Items[i].Text;
                            }
                        }
                    }
                }
            }


            return View(listaVM.ToPagedList(pageNumber, pageSize));
        }

        /* GET: Product/Details/5
        public ActionResult Details(int id)
        {
            var prodDto = _productBusiness.GetProduct(id);
            var prodVM = new ProductVM();
            AutoMapper.Mapper.Map(prodDto, prodVM);

            return View("ProductDetail",prodVM);
            //return PartialView("ModalProduct", prodVM);
        }*/

        // GET: Product/Details/5
        public ActionResult Details(int id)
        {
            var prodDto = _productBusiness.GetProduct(id);
            var prodVM = new ProductVM();
            AutoMapper.Mapper.Map(prodDto, prodVM);
            prodVM.ProveedorName=_supplierBusiness.GetSupplier(prodVM.ProveedorId).Name;
            prodVM.CategoryName = _categoryBusiness.GetCategory(prodVM.ProductType).Name;
            ViewBag.ModalName = "Detalles de Producto";
            ViewBag.GoTo = "Details";
            return PartialView("ModalProduct", prodVM);
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            ViewBag.ModalName = "Crear Producto";
            ViewBag.GoTo = "Create";
            ViewBag.typeSupplier = DataFilterSupplier();
            ViewBag.typeCategory = DataFilterCategory();
            return PartialView("ModalProduct");

        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(ProductVM prodVM,int filterTypeCategory, int filterTypeSupplier)
        {
            try
            {
                var prodDto = new ProductDto();
                prodVM.ProductType = filterTypeCategory;
                prodVM.ProveedorId = filterTypeSupplier;
                AutoMapper.Mapper.Map(prodVM, prodDto);
                var result = _productBusiness.InsertProduct(prodDto);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            var prodDto = _productBusiness.GetProduct(id);
            var prodVM = new ProductVM();
            AutoMapper.Mapper.Map(prodDto, prodVM);
            ViewBag.ModalName = "Editar Producto";
            ViewBag.typesrc = DataFilterSupplierEdit(prodVM.ProveedorId);
            ViewBag.typesupp = DataFilterCategory(prodVM.ProductType);
            ViewBag.GoTo = "Edit";
            return PartialView("ModalProduct", prodVM);
        }

        // POST: Product/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, ProductVM prodVM, int filterTypeCategory, int filterTypeSupplier)
            {
            try
            {
                var prodDto = new ProductDto();
                prodVM.ProveedorId = filterTypeSupplier;
                prodVM.ProductType = filterTypeCategory;
                AutoMapper.Mapper.Map(prodVM, prodDto);
                var result = _productBusiness.UpdateProduct(prodDto);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public bool Eliminar(int id)
        {
            try
            {
                //return true;
                var result = _productBusiness.DeleteProduct(id);
                if (result == "Succes")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int id)
        {
            var prodDto = _productBusiness.GetProduct(id);
            var prodVM = new ProductVM();
            AutoMapper.Mapper.Map(prodDto, prodVM);
            return View(prodVM);
        }

        // POST: Product/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, ProductVM prodVM)
        {
            try
            {
                var prodDto = new ProductDto();
                AutoMapper.Mapper.Map(prodVM, prodDto);
                var result = _productBusiness.DeleteProduct(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}