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
    public class ContactMessageController : Controller
    {
        IMessageInboxBusiness _contacMsgBusiness;
        public ContactMessageController(MessageInboxBusiness contacMsgBusiness)
        {
            _contacMsgBusiness = contacMsgBusiness;
        }

        // GET: ContactMessage
        public ActionResult Index()
        {
            var listaDto = _contacMsgBusiness.GetAllMessage();
            var listaVM = new List<ContactMessageVM>();
            AutoMapper.Mapper.Map(listaDto, listaVM);

            return View("Inbox",listaVM);
        }

        // GET: ContactMessage/Details/5
        public ActionResult Details(int id)
        {
            var itemDto = _contacMsgBusiness.GetMessage(id);
            //--------- Change status Isread
            itemDto.IsRead = true;
            var result = _contacMsgBusiness.UpdateMessage(itemDto);
            if (result == "Succes")
            {
                var itemVM = new ContactMessageVM();
                AutoMapper.Mapper.Map(itemDto, itemVM);
                return View("MessageDetail",itemVM);
            }
            else
            {
                return RedirectToAction("Index");
            }
           
        }

        // GET: ContactMessage/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ContactMessage/Create
        [HttpPost]
        public ActionResult Create(ContactMessageVM itemVM)
        {
            try
            {
                var itemDto = new MessageDto();
                AutoMapper.Mapper.Map(itemVM, itemDto);
                var result = _contacMsgBusiness.InsertMessage(itemDto);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ContactMessage/Edit/5
        public ActionResult Edit(int id)
        {
            var itemDto = _contacMsgBusiness.GetMessage(id);
            var itemVM = new ContactMessageVM();
            AutoMapper.Mapper.Map(itemDto, itemVM);
            return View(itemVM);
        }

        // POST: ContactMessage/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, ContactMessageVM itemVM)
        {
            try
            {
                var itemDto = new MessageDto();
                AutoMapper.Mapper.Map(itemVM, itemDto);
                var result = _contacMsgBusiness.UpdateMessage(itemDto);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ContactMessage/Delete/5
        public ActionResult Delete(int id)
        {
            var itemDto = _contacMsgBusiness.GetMessage(id);
            var itemVM = new ContactMessageVM();
            AutoMapper.Mapper.Map(itemDto, itemVM);
            return View(itemVM);
        }

        // POST: ContactMessage/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, ContactMessageVM itemVM)
        {
            try
            {
                var itemDto = new MessageDto();
                AutoMapper.Mapper.Map(itemVM, itemDto);
                var result = _contacMsgBusiness.DeleteMessage(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // POST: ContactMessage/Eliminar/5
        [HttpPost]
        public bool Eliminar(int id)
        {
            try
            {
                //return true;
                var result = _contacMsgBusiness.DeleteMessage(id);
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