using PagedList;
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