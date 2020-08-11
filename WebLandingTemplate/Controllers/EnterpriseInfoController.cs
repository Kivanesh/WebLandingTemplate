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
using WebLandingTemplate.Models;
using WebLandingTemplateBusinessLogic.Interface;
using WebLandingTemplateBusinessLogic.Logic;
using WebLandingTemplateDomainModel.Models;

namespace WebLandingTemplate.Controllers
{
    public class EnterpriseInfoController : Controller
    {
        IEnterpriseInfoBusiness _corpBusiness;
        public EnterpriseInfoController(EnterpriseInfoBusiness corpBusiness)
        {
            _corpBusiness = corpBusiness;
        }

        // GET: EnterpriseInfo
        public ActionResult Index()
        {
            var itemDto = _corpBusiness.GetEnterpriseInfo();
            var itemVM = new EnterpriseInfoVM();
            AutoMapper.Mapper.Map(itemDto, itemVM);

            return View(itemVM);
        }

        // GET: EnterpriseInfo/Details/5
        public ActionResult Details(int id)
        {
            var itemDto = _corpBusiness.GetEnterpriseInfo();
            var itemVM = new EnterpriseInfoVM();
            AutoMapper.Mapper.Map(itemDto, itemVM);

            return View(itemVM);
        }

        // GET: EnterpriseInfo/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EnterpriseInfo/Create
        [HttpPost]
        public ActionResult Create(EnterpriseInfoVM itemVM)
        {
            try
            {
                //var itemDto = new EnterpriseInfoDto();
                //AutoMapper.Mapper.Map(itemVM, itemDto);
                //var result = _corpBusiness.InsertItem(itemDto);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: EnterpriseInfo/Edit/5
        public ActionResult Edit(int id)
        {
            var itemDto = _corpBusiness.GetEnterpriseInfo();
            var itemVM = new EnterpriseInfoVM();
            AutoMapper.Mapper.Map(itemDto, itemVM);
            return View(itemVM);
        }

        // POST: EnterpriseInfo/Edit/5
        [HttpPost]
        public ActionResult Edit(EnterpriseInfoVM itemVM)
        {
            HttpFileCollectionBase collectionBase = Request.Files;

            try
            {
                string typeFile = collectionBase.Get(0).ContentType;
                if (collectionBase.Get(0).ContentLength > 0 && collectionBase.Get(0).ContentType == "image/jpeg")
                {
                 WebImage image = new WebImage(collectionBase.Get(0).InputStream);
                 itemVM.Logo = image.GetBytes();
                 
                }
                else//CUANDO NO ES JPG
                {
                    EnterpriseInfoDto enterprise = _corpBusiness.GetEnterpriseInfo();
                    itemVM.Logo = enterprise.Logo;

                    ///Debe mostral un modal de error
                    ///

                }
                // TODO: Add insert logic here
                var itemDto = new EnterpriseInfoDto();
                AutoMapper.Mapper.Map(itemVM, itemDto);
                var result = _corpBusiness.UpdateItem(itemDto);
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
        }


        public ActionResult getImage()
        {
            EnterpriseInfoDto supplier = _corpBusiness.GetEnterpriseInfo();

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
        //// GET: EnterpriseInfo/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    var itemDto = _corpBusiness.GetEnterpriseInfo();
        //    var itemVM = new EnterpriseInfoVM();
        //    AutoMapper.Mapper.Map(itemDto, itemVM);
        //    return View(itemVM);
        //}

        //// POST: EnterpriseInfo/Delete/5
        //[HttpPost]
        //public ActionResult Delete(int id, EnterpriseInfoVM itemVM)
        //{
        //    try
        //    {
        //        var itemDto = new EnterpriseInfoDto();
        //        AutoMapper.Mapper.Map(itemVM, itemDto);
        //        var result = _corpBusiness.DeleteItem(id);
        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

    }
}