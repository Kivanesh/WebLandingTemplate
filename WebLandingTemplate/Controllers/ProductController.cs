using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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

        // GET: Product
        [Authorize]
        public ActionResult Index()
        {
            var listaDto = _productBusiness.GetAllProducts();
            var listaVM = new List<ProductVM>();
            AutoMapper.Mapper.Map(listaDto, listaVM);

            return View(listaVM);
        }

        // GET: Product/Details/5
        public ActionResult Details(int id)
        {
            var prodDto = _productBusiness.GetProduct(id);
            var prodVM = new ProductVM();
            AutoMapper.Mapper.Map(prodDto, prodVM);

            return View("ProductDetail",prodVM);
            //return PartialView("ModalProduct", prodVM);
        }

        // GET: Product/Details/5
        public ActionResult DetailsModal(int id)
        {
            var prodDto = _productBusiness.GetProduct(id);
            var prodVM = new ProductVM();
            AutoMapper.Mapper.Map(prodDto, prodVM);

            return PartialView("ModalProduct", prodVM);
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            return View();
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
            return View("ProductDetail", prodVM);
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