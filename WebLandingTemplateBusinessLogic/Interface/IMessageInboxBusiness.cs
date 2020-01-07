using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebLandingTemplateDomainModel.Models;

namespace WebLandingTemplateBusinessLogic.Interface
{
    public interface IMessageInboxBusiness
    {
        List<MessageDto> GetAllMessage();
        string InsertMessage(MessageDto ObjModel);
        string UpdateMessage(MessageDto ObjModel);
        MessageDto GetMessage(int id);
        string DeleteMessage(int id);
        List<MessageDto> GetAllMessageNews();
    }
}
