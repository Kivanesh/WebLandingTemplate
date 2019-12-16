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
    public class ServiceCorpController : Controller
    {
        IServiceCorpBusiness _svcBusiness;
        public ServiceCorpController(ServiceCorpBusiness svcBusiness)
        {
            _svcBusiness = svcBusiness;
        }


        // GET: ServiceCorp
        public ActionResult Index()
        {
            var listaDto = _svcBusiness.GetAllService();
            var listaVM = new List<ServiceCorpVM>();
            AutoMapper.Mapper.Map(listaDto, listaVM);

            return View(listaVM);
        }

        // GET: ServiceCorp/Details/5
        public ActionResult Details(int id)
        {
            var prodDto = _svcBusiness.GetServiceCorp(id);
            var prodVM = new ServiceCorpVM();
            AutoMapper.Mapper.Map(prodDto, prodVM);

            return View(prodVM);
        }

        // GET: ServiceCorp/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ServiceCorp/Create
        [HttpPost]
        public ActionResult Create(ServiceCorpVM svcVM)
        {
            try
            {
                var svcDto = new ServiceCorpDto();
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
            var prodVM = new ServiceCorpVM();
            AutoMapper.Mapper.Map(svcDto, prodVM);
            return View(prodVM);
        }

        // POST: ServiceCorp/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, ServiceCorpVM svcVM)
        {
            try
            {
                var svcDto = new ServiceCorpDto();
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













    }
}