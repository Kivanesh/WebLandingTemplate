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
            try
            {
                var itemDto = new EnterpriseInfoDto();
                AutoMapper.Mapper.Map(itemVM, itemDto);
                var result = _corpBusiness.UpdateItem(itemDto);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
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