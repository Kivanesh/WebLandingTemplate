using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebLandingTemplateBusinessLogic.Interface;
using WebLandingTemplateBusinessLogic.Logic;

namespace WebLandingTemplate.Controllers
{
    public class HomeController : Controller
    {
        //Dependency Injection

        IEnterpriseInfoBusiness _corpBusiness;

        //Oop Principle: Depends on the abtraction not on the concrete classes

        public HomeController(EnterpriseInfoBusiness corpBusiness)
        {
            _corpBusiness = corpBusiness;
        }

        public ActionResult Index()
        {
            //Account/Login

            ViewBag.calis = _corpBusiness.GetEnterpriseInfo().Mision;

            return View();
        }

        public ActionResult Dashboard()
        {
            

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}