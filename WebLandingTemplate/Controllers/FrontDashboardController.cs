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
    public class FrontDashboardController : Controller
    {
        IFrontDashboardBusiness _frontDashboardBusiness;
        public FrontDashboardController(FrontDashboardBusiness frontDashboardBusiness)
        {
            _frontDashboardBusiness = frontDashboardBusiness;
        }

        // GET: FrontDashboard
        public ActionResult Index()
        {
            var listaDto = _frontDashboardBusiness.GetAllItems();
            var listaVM = new List<FrontDashboardVM>();
            AutoMapper.Mapper.Map(listaDto, listaVM);

            return View(listaVM);

        }

        // GET: FrontDashboard/Details/5
        public ActionResult Details(int id)
        {
            var itemDto = _frontDashboardBusiness.GetItem(id);
            var itemVM = new FrontDashboardVM();
            AutoMapper.Mapper.Map(itemDto, itemVM);

            return View(itemVM);
        }

        // GET: FrontDashboard/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FrontDashboard/Create
        [HttpPost]
        public ActionResult Create(FrontDashboardVM itemVM)
        {
            try
            {
                var itemDto = new FrontDashboardDto();
                AutoMapper.Mapper.Map(itemVM, itemDto);
                var result = _frontDashboardBusiness.InsertItem(itemDto);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: FrontDashboard/Edit/5
        public ActionResult Edit(int id)
        {
            var itemDto = _frontDashboardBusiness.GetItem(id);
            var itemVM = new FrontDashboardVM();
            AutoMapper.Mapper.Map(itemDto, itemVM);
            return View(itemVM);
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