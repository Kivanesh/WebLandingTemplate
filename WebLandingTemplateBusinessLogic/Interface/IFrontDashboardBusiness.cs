using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebLandingTemplateDomainModel.Models;

namespace WebLandingTemplateBusinessLogic.Interface
{
    public interface IFrontDashboardBusiness
    {
        List<FrontDashboardDto> GetAllItems();
        string InsertItem(FrontDashboardDto ObjModel);
        string UpdateItem(FrontDashboardDto ObjModel);
        FrontDashboardDto GetItem(int id);
        string DeleteItem(int id);
    }
}
