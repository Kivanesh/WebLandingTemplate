using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebLandingTemplateDomainModel.Models;

namespace WebLandingTemplateBusinessLogic.Interface
{
    public interface IServiceCorpBusiness
    {
        List<ServiceCorpDto> GetAllService();
        string InsertService(ServiceCorpDto ObjModel);
        string UpdateService(ServiceCorpDto ObjModel);
        ServiceCorpDto GetServiceCorp(int id);
        string DeleteService(int id);
    }
}
