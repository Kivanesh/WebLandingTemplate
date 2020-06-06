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
    public class ImageSrcController : Controller
    {
        IImageSrcBusiness _imageSrcBusiness;
        public ImageSrcController(ImageSrcBusiness imageSrcBusiness)
        {
            _imageSrcBusiness = imageSrcBusiness;
        }

        // GET: ImageSrc
        public ActionResult Index()
        {
            var listaDto = _imageSrcBusiness.GetAllItems();
            var listaVM = new List<ImageSrcVM>();
            AutoMapper.Mapper.Map(listaDto, listaVM);

            return View(listaVM);
        }

        // GET: ImageSrc/Details/5
        public ActionResult Details(int id)
        {
            var itemDto = _imageSrcBusiness.GetItem(id);
            var itemVM = new ImageSrcVM();
            AutoMapper.Mapper.Map(itemDto, itemVM);

            return View(itemVM);
        }

        // GET: ImageSrc/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ImageSrc/Create
        [HttpPost]
        public ActionResult Create(ImageSrcVM itemVM)
        {
            try
            {
                var itemDto = new ImageSrcDto();
                AutoMapper.Mapper.Map(itemVM, itemDto);
                var result = _imageSrcBusiness.InsertItem(itemDto);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ImageSrc/Edit/5
        public ActionResult Edit(int id)
            {
            var itemDto = _imageSrcBusiness.GetAllItems().Where(c => c.ItemId == id).ToList();
            var itemVM = new ImageSrcVM();
            AutoMapper.Mapper.Map(itemDto.First(), itemVM);
            ViewBag.ModalName = "Editar Dashboard";
            ViewBag.GoTo = "Edit";
            return PartialView("ModalImageSrc", itemVM);
        }

        // POST: ImageSrc/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, ImageSrcVM itemVM)
        {
            try
            {
                var itemDto = new ImageSrcDto();
                AutoMapper.Mapper.Map(itemVM, itemDto);
                var result = _imageSrcBusiness.UpdateItem(itemDto);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ImageSrc/Delete/5
        public ActionResult Delete(int id)
        {
            var itemDto = _imageSrcBusiness.GetItem(id);
            var itemVM = new ImageSrcVM();
            AutoMapper.Mapper.Map(itemDto, itemVM);
            return View(itemVM);
        }

        // POST: ImageSrc/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, ImageSrcVM itemVM)
        {
            try
            {
                var itemDto = new ImageSrcDto();
                AutoMapper.Mapper.Map(itemVM, itemDto);
                var result = _imageSrcBusiness.DeleteItem(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

    }
}