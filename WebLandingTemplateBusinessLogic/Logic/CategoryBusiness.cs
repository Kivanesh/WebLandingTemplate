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
    public class CategoryBusiness : ICategoryBusiness
    {

        private readonly IUnitOfWork unitOfwork;
        private readonly CategoryRepository categoryRepository;

        public CategoryBusiness(IUnitOfWork _unitOfwork)
        {
            unitOfwork = _unitOfwork;
            categoryRepository = new CategoryRepository(unitOfwork);

        }

        #region Product CRUD

        // ---------------------------------------------------- Create Method
        public string InsertCategory(CategoryDto ObjModel)
        {
            string result = string.Empty;
            try
            {
                Categories NewItem = new Categories()
                {
                    Name = ObjModel.Name,
                    ItemCodeType = ObjModel.ItemCodeType
                  
                };
                var record = categoryRepository.Insert(NewItem);
                result = (record == null) ? "Failed" : "Succes";
            }
            catch (Exception ex)
            {
                result = "Error: " + ex.Message;
            }
            return result;
            throw new NotImplementedException();
        }

        // ---------------------------------------------------- Retrive/Get Method
        public List<CategoryDto> GetAllCategory()
        {
            var ItemList = categoryRepository.GetAll().Select(x => new CategoryDto()
            {
                CategoryId   = x.CategoryId,
                Name         = x.Name,
                ItemCodeType = x.ItemCodeType
              
            }).ToList();

            return ItemList;
        }

        public CategoryDto GetCategory(int id)
        {
            var ItemDb = categoryRepository.SingleOrDefault(x => x.CategoryId == id);
            CategoryDto Item = new CategoryDto()
            {
                CategoryId   = ItemDb.CategoryId,
                Name         = ItemDb.Name,
                ItemCodeType = ItemDb.ItemCodeType
            };

            return Item;
        }

        // ---------------------------------------------------- Update Method
        public string UpdateCategory(CategoryDto ObjModel)
        {
            string result = string.Empty;
            try
            {
                Categories category = categoryRepository.SingleOrDefault(x => x.CategoryId == ObjModel.CategoryId);
                if (category != null)
                {
                    category.CategoryId = ObjModel.CategoryId;
                    category.Name = ObjModel.Name;
                    category.ItemCodeType = ObjModel.ItemCodeType;
                    categoryRepository.Update(category);
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
        public string DeleteCategory(int id)
        {
            string result = string.Empty;
            try
            {
                categoryRepository.Delete(x => x.CategoryId == id);
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
