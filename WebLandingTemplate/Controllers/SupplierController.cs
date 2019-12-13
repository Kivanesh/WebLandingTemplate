using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
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
            
            HttpFileCollectionBase collectionBase = Request.Files;
            WebImage image = new WebImage(collectionBase.Get(0).InputStream);
            supplier.Logo = image.GetBytes();
            try
            {
                // TODO: Add insert logic here
                var sup = _supplierBusiness.InsertSupplier(supplier);
                return RedirectToAction("Index");
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var entityValidationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in entityValidationErrors.ValidationErrors)
                    {
                        Response.Write("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                    }
                }
                return View();
            }
            /*catch
            {
                return View();
            }*/
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
                HttpFileCollectionBase collectionBase = Request.Files;
                WebImage image = new WebImage(collectionBase.Get(0).InputStream);
                supplier.Logo = image.GetBytes();
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


        public ActionResult getImage(int id)
        {
            SupplierDto supplier = _supplierBusiness.GetSupplier(id);
            byte[] byteImage = supplier.Logo;

            MemoryStream memoryStream = new MemoryStream(byteImage);
            Image image = Image.FromStream(memoryStream);
            memoryStream = new MemoryStream();
            image.Save(memoryStream, ImageFormat.Jpeg);
            memoryStream.Position = 0;
            return File(memoryStream,"image/jpg");
        }
    }
}
