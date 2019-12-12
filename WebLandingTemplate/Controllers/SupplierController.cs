using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebLandingTemplateBusinessLogic.Interface;
using WebLandingTemplateBusinessLogic.Logic;
using WebLandingTemplateDomainModel.Models;

namespace WebLandingTemplate.Controllers
{
    public class SupplierController : Controller
    {
        ISupplierBusiness _supplierBusiness;
        public SupplierController(SupplierBusiness supplierBusiness)
        {
            _supplierBusiness = supplierBusiness;
        }
        // GET: Supplier
        public ActionResult Index()
        {
            var lista = _supplierBusiness.GetAllSupplier();
            return View(lista);
        }

        // GET: Supplier/Details/5
        public ActionResult Details(int id)
        {
            var supp = _supplierBusiness.GetSupplier(id);
            return View(supp);
        }

        // GET: Supplier/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Supplier/Create
        [HttpPost]
        public ActionResult Create(SupplierDto supplier)
        {
            try
            {
                // TODO: Add insert logic here
                var sup = _supplierBusiness.InsertSupplier(supplier);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Supplier/Edit/5
        public ActionResult Edit(int id)
        {
            var supp = _supplierBusiness.GetSupplier(id);
            return View(supp);
        }

        // POST: Supplier/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, SupplierDto supplier)
        {
            try
            {
                // TODO: Add update logic here
                _supplierBusiness.UpdateSupplier(supplier);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Supplier/Delete/5
        public ActionResult Delete(int id)
        {
            var supp = _supplierBusiness.GetSupplier(id);

            return View(supp);
        }

        // POST: Supplier/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, SupplierDto supplier)
        {
            try
            {
                // TODO: Add delete logic here
                _supplierBusiness.DeleteSupplier(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
