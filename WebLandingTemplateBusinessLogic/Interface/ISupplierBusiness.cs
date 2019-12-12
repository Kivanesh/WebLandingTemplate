using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebLandingTemplateDomainModel.Models;

namespace WebLandingTemplateBusinessLogic.Interface
{
    public interface ISupplierBusiness
    {
        List<SupplierDto> GetAllSupplier();
        string InsertSupplier(SupplierDto suppModel);
        string UpdateSupplier(SupplierDto suppModel);
        SupplierDto GetSupplier(int id);
        string DeleteSupplier(int id);
    }
}
