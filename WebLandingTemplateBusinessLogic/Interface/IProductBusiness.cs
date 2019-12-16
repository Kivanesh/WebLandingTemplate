using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebLandingTemplateDomainModel.Models;

namespace WebLandingTemplateBusinessLogic.Interface
{
    public interface IProductBusiness
    {
        List<ProductDto> GetAllProducts();
        string InsertProduct(ProductDto ObjModel);
        string UpdateProduct(ProductDto ObjModel);
        ProductDto GetProduct(int id);
        string DeleteProduct(int id);
    }
}
