using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebLandingTemplateDomainModel.Models;

namespace WebLandingTemplateBusinessLogic.Interface
{
    public interface ICategoryBusiness
    {
        List<CategoryDto> GetAllCategory();
        string InsertCategory(CategoryDto ObjModel);
        string UpdateCategory(CategoryDto ObjModel);
        CategoryDto GetCategory(int id);
        string DeleteCategory(int id);
    }
}
