using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebLandingTemplate.Models.FrontVIewModels;
using WebLandingTemplateBusinessLogic.Interface;
using WebLandingTemplateBusinessLogic.Logic;

namespace WebLandingTemplate.Controllers.FrontEnd
{
    public class ProductFrontController : Controller
    {
        IProductBusiness _productBusiness;
        ISupplierBusiness _supplierBusiness;
        ICategoryBusiness _categoryBusiness;


        public ProductFrontController(ProductBusiness productBusiness, SupplierBusiness supplierBusiness, CategoryBusiness categoryBusiness)
        {
            _productBusiness = productBusiness;
            _supplierBusiness = supplierBusiness;
            _categoryBusiness = categoryBusiness;
        }
        // GET: ProductFront
        public ActionResult Index()
            {
            var listaDto = _categoryBusiness.GetAllCategory().ToList();
            var prodFrontVM = new ProductFrontVM();
           // listaDto = prodFrontVM.categoryVMs;
            AutoMapper.Mapper.Map(listaDto, prodFrontVM.categoryVMs);
           
            return View(prodFrontVM);
            }
        
    }
}