using PagedList;
using System;
using System.Collections.Generic;
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
    public class FrontDashboardController : Controller
    {
        IFrontDashboardBusiness _frontDashboardBusiness;
        IImageSrcBusiness _imageSrcBusiness;
        public FrontDashboardController(FrontDashboardBusiness frontDashboardBusiness, ImageSrcBusiness imageSrcBusiness)
        {
            _frontDashboardBusiness = frontDashboardBusiness;
            _imageSrcBusiness = imageSrcBusiness;
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

        // GET: FrontDashboard
        public ActionResult Index(int? page, string searchString, int pageSize = 3)
        {

            ViewBag.dropdownsrc = DataItems(pageSize);

            int pageNumber = (page ?? 1);
            var listaVM = new List<FrontDashboardVM>();

            if (!string.IsNullOrEmpty(searchString))
            {
                var listaDto = _frontDashboardBusiness.GetAllItems().Where(c => c.ElementName.Contains(searchString) || c.ItemImageId.ToString().Contains(searchString));
                AutoMapper.Mapper.Map(listaDto, listaVM);
            }
            else
            {
                var listaDto = _frontDashboardBusiness.GetAllItems();
                AutoMapper.Mapper.Map(listaDto, listaVM);
            }

            return View(listaVM.ToPagedList(pageNumber, pageSize));

        }

        // GET: FrontDashboard/Details/5
        public ActionResult Details(int id)
        {
            var itemDto = _frontDashboardBusiness.GetItem(id);
            var itemVM = new FrontDashboardVM();
            AutoMapper.Mapper.Map(itemDto, itemVM);

            //ViewBag.ModalName = "Detalles de Categoria";
            //ViewBag.GoTo = "Details";
            //return PartialView("ModalSupplier", itemVM);

            return View(itemVM);
        }

        // GET: FrontDashboard/Create
        public ActionResult Create()
        {
            ViewBag.ModalName = "Crear Nuevo Item";
            ViewBag.GoTo = "Create";
            return PartialView("ModalFrontDashboard");
        }

        // POST: FrontDashboard/Create
        [HttpPost]
        public ActionResult Create(FrontDashboardVM itemVM)
        {

            HttpFileCollectionBase collectionBase = Request.Files;
           
            try
            {
                if (collectionBase.Get(0).ContentLength > 0 && collectionBase.Get(0).ContentType == "image/jpeg")
                {
                    WebImage image = new WebImage(collectionBase.Get(0).InputStream);

                    //var result = _frontDashboardBusiness.GetAllItems();

                    var itemImageSrc = new ImageSrcDto();
                    itemImageSrc.Name = image.GetBytes();
                    itemImageSrc.ItemCodeType = 2;
                    itemImageSrc.Description = "Test";
                    var result = _imageSrcBusiness.InsertItem(itemImageSrc);
                    var itemDto = new FrontDashboardDto();
                    AutoMapper.Mapper.Map(itemVM, itemDto);
                    //var result = _frontDashboardBusiness.InsertItem(itemDto);
                    return RedirectToAction("Index");
                }
                else//CUANDO NO ES JPG
                {
                    return RedirectToAction("Index");

                    ///Debe mostral un modal de error
                    ///

                }
            }
            catch
            {
                return View();
            }
        }

        // GET: FrontDashboard/Edit/5
        public ActionResult Edit(int id)
        {
            //var itemDto = _frontDashboardBusiness.GetItem(id);
            //var itemVM = new FrontDashboardVM();
            //AutoMapper.Mapper.Map(itemDto, itemVM);
            var itemDto = _imageSrcBusiness.GetAllItems().Where(c => c.ItemId==id).ToList();
            var itemVM = new ImageSrcVM();
            AutoMapper.Mapper.Map(itemDto.First(), itemVM);
            ViewBag.ModalName = "Editar Dashboard";
            ViewBag.GoTo = "Edit";
            return PartialView("ModalFrontDashboard", itemVM);
        }

        // POST: FrontDashboard/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FrontDashboardVM itemVM)
        {
            try
            {
                var itemDto = new FrontDashboardDto();
                AutoMapper.Mapper.Map(itemVM, itemDto);
                var result = _frontDashboardBusiness.UpdateItem(itemDto);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: FrontDashboard/Delete/5
        public ActionResult Delete(int id)
        {
            var itemDto = _frontDashboardBusiness.GetItem(id);
            var itemVM = new FrontDashboardVM();
            AutoMapper.Mapper.Map(itemDto, itemVM);
            return View(itemVM);
        }
        public ActionResult getImage(int id)
        {
            ImageSrcDto imageDto = _imageSrcBusiness.GetItem(id);

            if (imageDto.Name != null)
            {

                if (imageDto.Name.Length > 0)
                {
                    byte[] byteImage = imageDto.Name;

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
        // POST: FrontDashboard/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FrontDashboardVM itemVM)
        {
            try
            {
                var itemDto = new FrontDashboardDto();
                AutoMapper.Mapper.Map(itemVM, itemDto);
                var result = _frontDashboardBusiness.DeleteItem(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

    }
}