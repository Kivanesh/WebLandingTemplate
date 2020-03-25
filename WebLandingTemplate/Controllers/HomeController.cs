using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using WebLandingTemplate.Models;
using WebLandingTemplate.Models.FrontVIewModels;
using WebLandingTemplateBusinessLogic.Interface;
using WebLandingTemplateBusinessLogic.Logic;
using WebLandingTemplateDomainModel.Models;

namespace WebLandingTemplate.Controllers
{
    public class HomeController : Controller
    {
        //Dependency Injection

        IEnterpriseInfoBusiness _corpBusiness;
        IMessageInboxBusiness _contacMsgBusiness;
        //Oop Principle: Depends on the abtraction not on the concrete classes

        public HomeController(EnterpriseInfoBusiness corpBusiness, MessageInboxBusiness contacMsgBusiness)
        {
            _corpBusiness = corpBusiness;
            _contacMsgBusiness = contacMsgBusiness;
        }

        public ActionResult Index()
        {
            // Build the VM 
            ContactPageVM contactPageVM = new ContactPageVM();
            var itemDto = _corpBusiness.GetEnterpriseInfo();
            var itemVM = new EnterpriseInfoVM();
            AutoMapper.Mapper.Map(itemDto, itemVM);
            contactPageVM.corpinfoVM = itemVM;
            contactPageVM.messageVM = new ContactMessageVM();
            
            itemDto = _corpBusiness.GetEnterpriseInfo();
            itemVM = new EnterpriseInfoVM();
            AutoMapper.Mapper.Map(itemDto, itemVM);
            contactPageVM.corpinfoVM = itemVM;
            return View(contactPageVM);
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
            // Build the VM 
            ContactPageVM contactPageVM = new ContactPageVM();
            var itemDto = _corpBusiness.GetEnterpriseInfo();
            var itemVM = new EnterpriseInfoVM();
            AutoMapper.Mapper.Map(itemDto, itemVM);
            contactPageVM.corpinfoVM = itemVM;
            contactPageVM.messageVM = new ContactMessageVM();
            return View(contactPageVM);
        }

        // POST: ContactMessage/Create
        [HttpPost]
        public ActionResult Contact(ContactPageVM PageVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var itemDto = new MessageDto();
                    AutoMapper.Mapper.Map(PageVM.messageVM, itemDto);
                    var senderEmail = new MailAddress("TestWebLanding@gmail.com", "Contacto Automatico desde Web Landing");
                    var receiverEmail = new MailAddress("test@example.com", "Receptor");
                    //var receiverEmail = new MailAddress("irvin_lechugasss@hotmail.com", "Receptor");
                    var password = "MagicPass_369";
                    var sub = itemDto.Subject;
                    var body = "From: " + itemDto.ContactName + "\n\n\t Email del Contacto: " + itemDto.Email + "\t Telefono: " + itemDto.Phone + "\n\n Mensaje: " + itemDto.Message;
                    var smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(senderEmail.Address, password)
                    };
                    using (var mess = new MailMessage(senderEmail, receiverEmail)
                    {
                        Subject = sub,
                        Body = body
                    })
                        smtp.Send(mess);
                    var result = _contacMsgBusiness.InsertMessage(itemDto);
                    if (result == "Succes")
                    {
                        ViewBag.Result = (result == null) ? false : true;
                        return PartialView("EmailResponse");
                    }
                    else
                    {
                        ViewBag.Result = false;
                        return PartialView("EmailResponse");
                    }
                        
                }
            }
            catch (Exception ex)
            {
                ViewBag.Result = false;
                return PartialView("EmailResponse");
            }
            return RedirectToAction("Contact");
        }



    }
}