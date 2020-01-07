using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebLandingTemplateBusinessLogic.Interface;
using WebLandingTemplateDomainModel.Models;
using WebLandingTemplateRepository;
using WebLandingTemplateRepository.Infrastructure.Contract;
using WebLandingTemplateRepository.Repository;

namespace WebLandingTemplateBusinessLogic.Logic
{
    public class MessageInboxBusiness : IMessageInboxBusiness
    {

        private readonly IUnitOfWork unitOfwork;
        private readonly MessageInboxRepository messageRepository;

        public MessageInboxBusiness(IUnitOfWork _unitOfwork)
        {
            unitOfwork = _unitOfwork;
            messageRepository = new MessageInboxRepository(unitOfwork);

        }

        #region Message Inbox CRUD

        // ---------------------------------------------------- Create Method
        public string InsertMessage(MessageDto ObjModel)
        {
            string result = string.Empty;
            try
            {
                ContactMessageInbox NewItem = new ContactMessageInbox()
                {
                    Subject = ObjModel.Subject,
                    Email = ObjModel.Email,
                    ContactName = ObjModel.ContactName,
                    Message = ObjModel.Message,
                    Phone = ObjModel.Phone,
                    ContactDate = ObjModel.ContactDate,
                    ComeFrom = ObjModel.ComeFrom,
                    IsRead = ObjModel.IsRead
                };
                var record = messageRepository.Insert(NewItem);
                result = (record == null) ? "Failed" : "Succes";
            }
            catch (Exception ex)
            {
                result = "Error: " + ex.Message;
            }
            return result;
        }

        // ---------------------------------------------------- Retrive/Get Method
        public List<MessageDto> GetAllMessage()
        {
            var ItemList = messageRepository.GetAll().Select(x => new MessageDto()
            {
                MessageId   = x.MessageId,
                Subject     = x.Subject,
                Email       = x.Email,
                ContactName = x.ContactName,
                Message     = x.Message,
                Phone       = x.Phone,
                ContactDate = x.ContactDate,
                ComeFrom    = x.ComeFrom,
                IsRead      = x.IsRead
                

            }).ToList();

            return ItemList;
        }

        public MessageDto GetMessage(int id)
        {
            var ItemDb = messageRepository.SingleOrDefault(x => x.MessageId == id);
            MessageDto Item = new MessageDto()
            {
                MessageId   = ItemDb.MessageId,
                Subject     = ItemDb.Subject,
                Email       = ItemDb.Email,
                ContactName = ItemDb.ContactName,
                Message     = ItemDb.Message,
                Phone       = ItemDb.Phone,
                ContactDate = ItemDb.ContactDate,
                ComeFrom    = ItemDb.ComeFrom,
                IsRead      = ItemDb.IsRead
            };

            return Item;
        }

        // ---------------------------------------------------- Update Method
        public string UpdateMessage(MessageDto ObjModel)
        {
            string result = string.Empty;
            try
            {
                ContactMessageInbox message = messageRepository.SingleOrDefault(x => x.MessageId == ObjModel.MessageId);
                if (message != null)
                {
                    message.Subject = ObjModel.Subject;
                    message.Email = ObjModel.Email;
                    message.ContactName = ObjModel.ContactName;
                    message.Message = ObjModel.Message;
                    message.Phone = ObjModel.Phone;
                    message.ContactDate = ObjModel.ContactDate;
                    message.ComeFrom = ObjModel.ComeFrom;
                    message.IsRead = ObjModel.IsRead;
                    messageRepository.Update(message);
                    result = "Succes";
                }
                else
                {
                    result = "Failed";
                }
            }
            catch (Exception ex)
            {
                result = "Error: " + ex.Message;
            }
            return result;
        }

        // ---------------------------------------------------- Delete Method
        public string DeleteMessage(int id)
        {
            string result = string.Empty;
            try
            {
                messageRepository.Delete(x => x.MessageId == id);
                result = "Succes";
            }
            catch (Exception ex)
            {
                result = "Error: " + ex.Message;
            }
            return result;
        }

        #endregion

        public List<MessageDto> GetAllMessageNews()
        {
            var ItemList = messageRepository.GetAll().Where(x => x.IsRead == false).Select(x => new MessageDto()
            {
                MessageId = x.MessageId,
                Subject = x.Subject,
                Email = x.Email,
                ContactName = x.ContactName,
                Message = x.Message,
                Phone = x.Phone,
                ContactDate = x.ContactDate,
                ComeFrom = x.ComeFrom,
                IsRead = x.IsRead
            }).ToList();

            return ItemList;
        }

    }
}
