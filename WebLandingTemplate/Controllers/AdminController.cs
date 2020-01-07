using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebLandingTemplateBusinessLogic.Interface;
using WebLandingTemplateBusinessLogic.Logic;

namespace WebLandingTemplate.Controllers
{
    public class AdminController : Controller
    {
        IMessageInboxBusiness _contacMsgBusiness;

        public AdminController(MessageInboxBusiness contacMsgBusiness)
        {
            _contacMsgBusiness = contacMsgBusiness;
        }


        // GET: Admin
        public ActionResult Index()
        {
            ViewBag.Inbox = _contacMsgBusiness.GetAllMessageNews();
            return View();
        }
    }
}