﻿using System;
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
    public class ServiceCorpController : Controller
    {
        IServiceCorpBusiness _svcBusiness;
        ICategoryBusiness _categoryBusiness;

        public ServiceCorpController(ServiceCorpBusiness svcBusiness,CategoryBusiness categoryBusiness)
        {
            _svcBusiness = svcBusiness;
            _categoryBusiness = categoryBusiness;
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

        private IEnumerable<SelectListItem> DataFilterCategory()
        {
            //var istenum = Enum.GetValues(typeof(ItemCodeTypeEnum)).Cast<ItemCodeTypeEnum>().Select(p => new SelectListItem()
            var istenum = _categoryBusiness.GetAllCategory().Where(p => p.ItemCodeType == 2 ).Select(p => new SelectListItem()

            {
                Text = p.Name,
                Value = ((int)p.CategoryId).ToString()
            }).ToList();
            istenum.Insert(0, new SelectListItem() { Value = null, Text = "--Seleciona--", Selected = true });

            return new SelectList(istenum, "Value", "Text", "Selected");
        }

        // GET: ServiceCorp
        [Authorize]
        public ActionResult Index(int? page, string searchString, int pageSize = 3)
        {
            ViewBag.dropdownsrc = DataItems(pageSize);

            int pageNumber = (page ?? 1);
            var listaVM = new List<ServiceCorpVM>();

            if (!string.IsNullOrEmpty(searchString))
            {
                var listaDto = _svcBusiness.GetAllService().Where(c => c.Name.Contains(searchString) || c.Description.Contains(searchString));
                AutoMapper.Mapper.Map(listaDto, listaVM);
            }
            else
            {
                var listaDto = _svcBusiness.GetAllService();
                AutoMapper.Mapper.Map(listaDto, listaVM);
            }

            return View(listaVM.ToPagedList(pageNumber, pageSize));
        }

        // GET: ServiceCorp/Details/5
        public ActionResult Details(int id)
        {
            var prodDto = _svcBusiness.GetServiceCorp(id);
            var prodVM = new ServiceCorpVM();
            AutoMapper.Mapper.Map(prodDto, prodVM);
            prodVM.CategoryName = _categoryBusiness.GetCategory(prodVM.ServiceType).Name;
            ViewBag.ModalName = "Detalles de Servicio";
            ViewBag.GoTo = "Details";
            return PartialView("ModalServiceCorp", prodVM);
        }

        // GET: ServiceCorp/Create
        public ActionResult Create()
        {
            ViewBag.ModalName = "Crear Servicio";
            ViewBag.GoTo = "Create";
            ViewBag.typescat = DataFilterCategory();
            return PartialView("ModalServiceCorp");
        }

        // POST: ServiceCorp/Create
        [HttpPost]
        public ActionResult Create(ServiceCorpVM svcVM, int filterTypeCategory)
        {
            try
            {
                var svcDto = new ServiceCorpDto();
                svcVM.ServiceType = filterTypeCategory;
                AutoMapper.Mapper.Map(svcVM, svcDto);
                var result = _svcBusiness.InsertService(svcDto);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ServiceCorp/Edit/5
        public ActionResult Edit(int id)
        {
            var svcDto = _svcBusiness.GetServiceCorp(id);
            var servVM = new ServiceCorpVM();
            AutoMapper.Mapper.Map(svcDto, servVM);
            ViewBag.typescat = DataFilterCategory();
            ViewBag.ModalName = "Editar Servicio";
            ViewBag.GoTo = "Edit";
            return PartialView("ModalServiceCorp",servVM);
        }

        // POST: ServiceCorp/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, ServiceCorpVM svcVM, int filterTypeCategory)
        {
            try
            {
                var svcDto = new ServiceCorpDto();
                svcVM.ServiceType = filterTypeCategory;
                AutoMapper.Mapper.Map(svcVM, svcDto);
                var result = _svcBusiness.UpdateService(svcDto);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ServiceCorp/Delete/5
        public ActionResult Delete(int id)
        {
            var svcDto = _svcBusiness.GetServiceCorp(id);
            var prodVM = new ProductVM();
            AutoMapper.Mapper.Map(svcDto, prodVM);
            return View(prodVM);
        }

        // POST: ServiceCorp/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, ServiceCorpVM svcVM)
        {
            try
            {
                var svcDto = new ServiceCorpDto();
                AutoMapper.Mapper.Map(svcVM, svcDto);
                var result = _svcBusiness.DeleteService(id);
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
                var result = _svcBusiness.DeleteService(id);
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













    }
}