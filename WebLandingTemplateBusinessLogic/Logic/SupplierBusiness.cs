using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebLandingTemplateBusinessLogic.Interface;
using WebLandingTemplateDomainModel.Models;
using WebLandingTemplateRepository.Infrastructure.Contract;
using WebLandingTemplateRepository.Repository;
using WebLandingTemplateRepository;

namespace WebLandingTemplateBusinessLogic.Logic
{
    public class SupplierBusiness : ISupplierBusiness
    {
        private readonly IUnitOfWork unitOfwork;
        private readonly SupplierRepository supplierRepository;

        public SupplierBusiness(IUnitOfWork _unitOfwork)
        {
            unitOfwork = _unitOfwork;
            supplierRepository = new SupplierRepository(unitOfwork);

        }


        public List<SupplierDto> GetAllSupplier()
        {
            List<SupplierDto> Suppliers = supplierRepository.GetAll().Select(m => new SupplierDto()
            {
                ProveedorId = m.ProveedorId,
                Name = m.Name,
                Logo = m.Logo,
                Descripcion = m.Descripcion
            }).ToList();

            return Suppliers;
        }
        public string InsertSupplier(SupplierDto suppModel)
        {

            string result = "";


            suppliers sup = new suppliers();

            sup.Name = suppModel.Name;
            sup.Logo = suppModel.Logo;
            sup.Descripcion = suppModel.Descripcion;




            var record = supplierRepository.Insert(sup);

            result = "Inserted";


            return result;
        }
        public string UpdateSupplier(SupplierDto suppModel)
        {
            string result = "";
            if (suppModel.ProveedorId > 0)
            {

                suppliers supp = supplierRepository.SingleOrDefault(x => x.ProveedorId == suppModel.ProveedorId);

                if (supp != null)
                {
                    supp.Name = suppModel.Name;
                    supp.Logo = suppModel.Logo;
                    supp.Descripcion = suppModel.Descripcion;
                    supplierRepository.Update(supp);

                    result = "updated";

                }
            }
            return result;
        }
        public SupplierDto GetSupplier(int id)
        {

            suppliers supp = supplierRepository.SingleOrDefault(x => x.ProveedorId == id);
            SupplierDto supplierDto = new SupplierDto()
            {
                ProveedorId = supp.ProveedorId,
                Name = supp.Name,
                Logo = supp.Logo,
                Descripcion = supp.Descripcion

            };
            return supplierDto;
        }

        public string DeleteSupplier(int id)
        {
            supplierRepository.Delete(x => x.ProveedorId == id);
            return "delete";
        }
    }
}
