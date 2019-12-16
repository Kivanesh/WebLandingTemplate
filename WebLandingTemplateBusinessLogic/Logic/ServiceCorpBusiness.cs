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
    public class ServiceCorpBusiness : IServiceCorpBusiness
    {
        private readonly IUnitOfWork unitOfwork;
        private readonly ServiceCorpRepository servicecorpRepository;

        public ServiceCorpBusiness(IUnitOfWork _unitOfwork)
        {
            unitOfwork = _unitOfwork;
            servicecorpRepository = new ServiceCorpRepository(unitOfwork);

        }
       
        #region ServiceCorp CRUD

        // ---------------------------------------------------- Create Method
        public string InsertService(ServiceCorpDto ObjModel)
        {
            string result = string.Empty;
            try
            {
                ServiceCorp NewItem = new ServiceCorp()
                {
                    Name = ObjModel.Name,
                    Description = ObjModel.Description,
                    ServiceType = ObjModel.ServiceType
                    

                };
                var record = servicecorpRepository.Insert(NewItem);
                result = (record == null) ? "Failed" : "Succes";
            }
            catch (Exception ex)
            {
                result = "Error: " + ex.Message;
            }
            return result;
        }

        // ---------------------------------------------------- Retrive/Get Method
        public List<ServiceCorpDto> GetAllService()
        {
            var ItemList = servicecorpRepository.GetAll().Select(x => new ServiceCorpDto()
            {
                ServiceId = x.ServiceId,
                Name = x.Name,
                Description = x.Description,
                ServiceType = x.ServiceType,

            }).ToList();

            return ItemList;
        }

        public ServiceCorpDto GetServiceCorp(int id)
        {
            var ItemDb = servicecorpRepository.SingleOrDefault(x => x.ServiceId == id);
            ServiceCorpDto Item = new ServiceCorpDto()
            {
                ServiceId = ItemDb.ServiceId,
                Name = ItemDb.Name,
                Description = ItemDb.Description,
                ServiceType = ItemDb.ServiceType,
            };

            return Item;
        }

        // ---------------------------------------------------- Update Method
        public string UpdateService(ServiceCorpDto ObjModel)
        {
            string result = string.Empty;
            try
            {
                ServiceCorp serviceCorp = servicecorpRepository.SingleOrDefault(x => x.ServiceId == ObjModel.ServiceId);
                if (serviceCorp != null)
                {
                    serviceCorp.ServiceId = ObjModel.ServiceId;
                    serviceCorp.Name = ObjModel.Name;
                    serviceCorp.Description = ObjModel.Description;
                    serviceCorp.ServiceType = ObjModel.ServiceType;
                    servicecorpRepository.Update(serviceCorp);
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
        public string DeleteService(int id)
        {
            string result = string.Empty;
            try
            {
                servicecorpRepository.Delete(x => x.ServiceId == id);
                result = "Succes";
            }
            catch (Exception ex)
            {
                result = "Error: " + ex.Message;
            }
            return result;
        }

        #endregion

    }
}
