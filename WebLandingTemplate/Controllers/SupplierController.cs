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
    public class SupplierController : Controller
    {
        ISupplierBusiness _supplierBusiness;
        public SupplierController(SupplierBusiness supplierBusiness)
        {
            _supplierBusiness = supplierBusiness;
        }
        // GET: Supplier
        public ActionResult Index(int? page, string searchString, int pageSize = 5)
        {
            // searchString = "vende"; 
            int pageNumber = (page ?? 1);

            var listaVM = new List<SupplierVM>();

            if (!string.IsNullOrEmpty(searchString))
            {
                var listaDto = _supplierBusiness.GetAllSupplier().Where(c => c.Name.Contains(searchString) || c.Descripcion.Contains(searchString));
                AutoMapper.Mapper.Map(listaDto, listaVM);
            }
            else
            {
                var listaDto = _supplierBusiness.GetAllSupplier();
                AutoMapper.Mapper.Map(listaDto, listaVM);
            }

            return View(listaVM.ToPagedList(pageNumber, pageSize));
        }

        // GET: Supplier/Details/5
        public ActionResult Details(int id)
        {
            var supDto = _supplierBusiness.GetSupplier(id);
            var suppVM = new SupplierVM();
            AutoMapper.Mapper.Map(supDto, suppVM);
            return View(suppVM);
        }

        // GET: Supplier/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Supplier/Create
        [HttpPost]
        public ActionResult Create(SupplierVM supplierVM)
        {

            HttpFileCollectionBase collectionBase = Request.Files;
            if (collectionBase.Get(0).ContentLength > 0 && collectionBase.Get(0).ContentType == "image/jpeg")
            {
                WebImage image = new WebImage(collectionBase.Get(0).InputStream);
                supplierVM.Logo = image.GetBytes();
            }
            try
            {
                // TODO: Add insert logic here
                var suppDto = new SupplierDto();
                AutoMapper.Mapper.Map(supplierVM, suppDto);
                var result = _supplierBusiness.InsertSupplier(suppDto);
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
            var suppDto = _supplierBusiness.GetSupplier(id);
            var suppVM = new SupplierVM();
            AutoMapper.Mapper.Map(suppDto, suppVM);
            return View(suppVM);
        }

        // POST: Supplier/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, SupplierVM supplierVM)
        {
            try
            {
                HttpFileCollectionBase collectionBase = Request.Files;
                string typeFile = collectionBase.Get(0).ContentType;
                if (collectionBase.Get(0).ContentLength > 0 && collectionBase.Get(0).ContentType == "image/jpeg")
                {
                    WebImage image = new WebImage(collectionBase.Get(0).InputStream);
                    supplierVM.Logo = image.GetBytes();
                }
                var supplierDto = new SupplierDto();
                // TODO: Add update logic here
                AutoMapper.Mapper.Map(supplierVM, supplierDto);
                _supplierBusiness.UpdateSupplier(supplierDto);
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
            var suppDto = _supplierBusiness.GetSupplier(id);
            var suppVM = new SupplierVM();
            AutoMapper.Mapper.Map(suppDto, suppVM);

            return View(suppVM);
        }

        // POST: Supplier/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, SupplierVM supplierVM)
        {
            try
            {
                // TODO: Add delete logic here
                var suppDto = new SupplierDto();
                AutoMapper.Mapper.Map(supplierVM, suppDto);
                var result = _supplierBusiness.DeleteSupplier(id);
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

            if (supplier.Logo != null)
            {

                if (supplier.Logo.Length > 0)
                {
                    byte[] byteImage = supplier.Logo;

                    MemoryStream memoryStream = new MemoryStream(byteImage);
                    Image image = Image.FromStream(memoryStream);
                    memoryStream = new MemoryStream();
                    image.Save(memoryStream, ImageFormat.Jpeg);
                    memoryStream.Position = 0;
                    return File(memoryStream, "image/jpg");
                }

                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }

        }
    }
}
