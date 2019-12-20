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
    public class FrontDashboardBusiness : IFrontDashboardBusiness
    {

        private readonly IUnitOfWork unitOfwork;
        private readonly FrontDashboardRepository frontRepository;

        public FrontDashboardBusiness(IUnitOfWork _unitOfwork)
        {
            unitOfwork = _unitOfwork;
            frontRepository = new FrontDashboardRepository(unitOfwork);

        }

        #region Front Dashboard CRUD

        // ---------------------------------------------------- Create Method
        public string InsertItem(FrontDashboardDto ObjModel)
        {
            string result = string.Empty;
            try
            {
                Dashboard NewItem = new Dashboard()
                {
                    //ItemImageId = ObjModel.ItemImageId, -> key
                    ElementName = ObjModel.ElementName,
                    ElementId   = ObjModel.ElementId
                };
                var record = frontRepository.Insert(NewItem);
                result = (record == null) ? "Failed" : "Succes";
            }
            catch (Exception ex)
            {
                result = "Error: " + ex.Message;
            }
            return result;
        }

        // ---------------------------------------------------- Retrive/Get Method
        public List<FrontDashboardDto> GetAllItems()
        {
            var ItemList = frontRepository.GetAll().Select(x => new FrontDashboardDto()
            {
                ItemImageId = x.ItemImageId,
                ElementName = x.ElementName,
                ElementId   = x.ElementId
            }).ToList();

            return ItemList;

        }

        public FrontDashboardDto GetItem(int id)
        {
            var ItemDb = frontRepository.SingleOrDefault(x => x.ItemImageId == id);
            FrontDashboardDto Item = new FrontDashboardDto()
            {
                ItemImageId = ItemDb.ItemImageId,
                ElementName = ItemDb.ElementName,
                ElementId   = ItemDb.ElementId
            };

            return Item;
        }

        // ---------------------------------------------------- Update Method
        public string UpdateItem(FrontDashboardDto ObjModel)
        {
            string result = string.Empty;
            try
            {
                Dashboard item = frontRepository.SingleOrDefault(x => x.ItemImageId == ObjModel.ItemImageId);
                if (item != null)
                {
                    item.ItemImageId = ObjModel.ItemImageId;
                    item.ElementName = ObjModel.ElementName;
                    item.ElementId = ObjModel.ElementId;
                    frontRepository.Update(item);
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
        public string DeleteItem(int id)
        {
            string result = string.Empty;
            try
            {
                frontRepository.Delete(x => x.ItemImageId == id);
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
