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
    public class ImageSrcBusiness : IImageSrcBusiness
    {

        private readonly IUnitOfWork unitOfwork;
        private readonly ImageSrcRepository imgRepository;

        public ImageSrcBusiness(IUnitOfWork _unitOfwork)
        {
            unitOfwork = _unitOfwork;
            imgRepository = new ImageSrcRepository(unitOfwork);

        }

        #region Message Inbox CRUD

        // ---------------------------------------------------- Create Method
        public string InsertItem(ImageSrcDto ObjModel)
        {
            string result = string.Empty;
            try
            {
                ImageSources NewItem = new ImageSources()
                {
                    //ImageId      = ObjModel.ImageId, -> key
                    ItemId = ObjModel.ItemId,
                    Name         = ObjModel.Name,
                    Description  = ObjModel.Description,
                    ItemCodeType = ObjModel.ItemCodeType
                };
                var record = imgRepository.Insert(NewItem);
                result = (record == null) ? "Failed" : "Succes";
            }
            catch (Exception ex)
            {
                result = "Error: " + ex.Message;
            }
            return result;
        }

        // ---------------------------------------------------- Retrive/Get Method
        public List<ImageSrcDto> GetAllItems()
        {
            var ItemList = imgRepository.GetAll().Select(x => new ImageSrcDto()
            {
                ImageId      = x.ImageId,
                ItemId       = x.ItemId,
                Name         = x.Name,
                Description  = x.Description,
                ItemCodeType = x.ItemCodeType
            }).ToList();

            return ItemList;
        }

        public ImageSrcDto GetItem(int id)
        {
            var ItemDb = imgRepository.SingleOrDefault(x => x.ImageId == id);
            ImageSrcDto Item = new ImageSrcDto()
            {
                ImageId      = ItemDb.ImageId,
                ItemId       = ItemDb.ItemId,
                Name         = ItemDb.Name,
                Description  = ItemDb.Description,
                ItemCodeType = ItemDb.ItemCodeType
            };

            return Item;
        }

        // ---------------------------------------------------- Update Method
        public string UpdateItem(ImageSrcDto ObjModel)
        {
            string result = string.Empty;
            try
            {
                ImageSources item = imgRepository.SingleOrDefault(x => x.ImageId == ObjModel.ImageId);
                if (item != null)
                {
                    item.ImageId = ObjModel.ImageId;
                    item.ItemId = ObjModel.ItemId;
                    item.Name = ObjModel.Name;
                    item.Description = ObjModel.Description;
                    item.ItemCodeType = ObjModel.ItemCodeType;
                    imgRepository.Update(item);
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
                imgRepository.Delete(x => x.ImageId == id);
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
