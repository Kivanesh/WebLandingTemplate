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
        IProductBusiness _productBusiness;


        public SupplierController(SupplierBusiness supplierBusiness, ProductBusiness productBusiness)
        {
            _supplierBusiness = supplierBusiness;
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

        // GET: Supplier
        public ActionResult Index(int? page, string searchString, int pageSize = 3)
        {
            ViewBag.dropdownsrc = DataItems(pageSize);

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
            ViewBag.ModalName = "Detalles de Categoria";
            ViewBag.GoTo = "Details";
            return PartialView("ModalSupplier", suppVM);

        }

        // GET: Supplier/Create
        public ActionResult Create()
        {
            ViewBag.ModalName = "Crear Proveedor";
            ViewBag.GoTo = "Create";
            return PartialView("ModalSupplier");

            //return View();

        }

        // POST: Supplier/Create
        [HttpPost]
        public ActionResult Create(SupplierVM supplierVM)
        {

            HttpFileCollectionBase collectionBase = Request.Files;

            try
            {
                if (collectionBase.Get(0).ContentLength > 0 && collectionBase.Get(0).ContentType == "image/jpeg")
                {
                    WebImage image = new WebImage(collectionBase.Get(0).InputStream);
                    supplierVM.Logo = image.GetBytes();
                    var suppDto = new SupplierDto();
                    AutoMapper.Mapper.Map(supplierVM, suppDto);
                    var result = _supplierBusiness.InsertSupplier(suppDto);
                    return RedirectToAction("Index");
                }
                else//CUANDO NO ES JPG
                {
                    return RedirectToAction("Index");

                    ///Debe mostral un modal de error
                    ///

                }
                // TODO: Add insert logic here
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
            ViewBag.ModalName = "Editar Proveedor";
            ViewBag.GoTo = "Edit";
            //return PartialView(itemVM);
            return PartialView("ModalSupplier", suppVM);
            //return View(itemVM);
        }

        // POST: Supplier/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, SupplierVM supplierVM)
        {
            HttpFileCollectionBase collectionBase = Request.Files;
            try
            {
                supplierVM.ProveedorId = id;

                
                string typeFile = collectionBase.Get(0).ContentType;
                if (collectionBase.Get(0).ContentLength > 0 && collectionBase.Get(0).ContentType == "image/jpeg")
                {
                    WebImage image = new WebImage(collectionBase.Get(0).InputStream);
                    supplierVM.Logo = image.GetBytes();
                }
                else
                {
                    SupplierDto supplier = _supplierBusiness.GetSupplier(id);
                    supplierVM.Logo = supplier.Logo;
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
        /* [HttpPost]
         public ActionResult Delete(int id, SupplierVM supplierVM)
         {
             try
             {
                 // TODO: Add delete logic here
                // var suppDto = new SupplierDto();
                 //AutoMapper.Mapper.Map(supplierVM, suppDto);
                 var result = _supplierBusiness.DeleteSupplier(id);

                 return RedirectToAction("Index");
             }
             catch
             {
                 return View();
             }
         }*/

        // POST: ContactMessage/Eliminar/5
        [HttpPost]
        public bool Eliminar(int id)
        {
            try
            {

                //Funtion to assign to "Sin Proveedor"
                //
                 var itemLstP = _productBusiness.GetAllProducts().Where(c => c.ProveedorId == id).ToList();
                    foreach (ProductDto i in itemLstP)
                    {
                        i.ProveedorId = 14;
                        var res = _productBusiness.UpdateProduct(i);
                    }
                
                var result = _supplierBusiness.DeleteSupplier(id);
                if (result == "Succes")
                {
                   
                    return true;
                }
                else
                {
                    foreach (ProductDto i in itemLstP)
                    {
                        i.ProveedorId = id;
                        var res = _productBusiness.UpdateProduct(i);
                    }
                    return false;
                }
            }
            catch
            {
                return false;
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
