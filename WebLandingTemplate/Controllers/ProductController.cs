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

namespace WebLandingTemplate.Controllers
{
    public class ProductController : Controller
    {
        IProductBusiness _productBusiness;
        public ProductController(ProductBusiness productBusiness)
        {
            _productBusiness = productBusiness;
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

        // GET: Product
        [Authorize]
        public ActionResult Index(int? page, string searchString, int pageSize = 3)
        {

            ViewBag.dropdownsrc = DataItems(pageSize);

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
            ViewBag.ModalName = "Detalles de Producto";
            ViewBag.GoTo = "Details";
            return PartialView("ModalProduct", prodVM);
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            ViewBag.ModalName = "Crear Producto";
            ViewBag.GoTo = "Create";
            return PartialView("ModalProduct");

        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(ProductVM prodVM)
        {
            try
            {
                var prodDto = new ProductDto();
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
            ViewBag.ModalName = "Editar Categoría";
            ViewBag.GoTo = "Edit";
            return PartialView("ModalProduct", prodVM);
        }

        // POST: Product/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, ProductVM prodVM)
            {
            try
            {
                var prodDto = new ProductDto();
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