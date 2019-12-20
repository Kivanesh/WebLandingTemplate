using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebLandingTemplateDomainModel.Models;

namespace WebLandingTemplateBusinessLogic.Interface
{
    public interface IImageSrcBusiness
    {
        List<ImageSrcDto> GetAllItems();
        string InsertItem(ImageSrcDto ObjModel);
        string UpdateItem(ImageSrcDto ObjModel);
        ImageSrcDto GetItem(int id);
        string DeleteItem(int id);
    }
}
