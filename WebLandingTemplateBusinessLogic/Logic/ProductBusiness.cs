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
    public class ProductBusiness : IProductBusiness
    {

        private readonly IUnitOfWork unitOfwork;
        private readonly ProductRepository productRepository;

        public ProductBusiness(IUnitOfWork _unitOfwork)
        {
            unitOfwork = _unitOfwork;
            productRepository = new ProductRepository(unitOfwork);

        }

        #region Product CRUD

        // ---------------------------------------------------- Create Method
        public string InsertProduct(ProductDto ObjModel)
        {
            string result = string.Empty;
            try
            {
                Products NewProd = new Products()
                {
                    ProductName = ObjModel.ProductName,
                    ProveedorId = ObjModel.ProveedorId,
                    Price = ObjModel.Price,
                    Description = ObjModel.Description,
                    Size = ObjModel.Size,
                    Color = ObjModel.Color,
                    ProductType = ObjModel.ProductType
                };
                var record = productRepository.Insert(NewProd);
                result = (record == null) ? "Failed" : "Succes";
            }
            catch (Exception ex)
            {
                result = "Error: " + ex.Message;
            }
            return result;
        }

        // ---------------------------------------------------- Retrive/Get Method
        public ProductDto GetProduct(int id)
        {
            var ItemDb = productRepository.SingleOrDefault(x => x.ProductId == id);
            ProductDto Item = new ProductDto()
            {
                ProductId   = ItemDb.ProductId,
                ProductName = ItemDb.ProductName,
                ProveedorId = ItemDb.ProveedorId,
                Price       = ItemDb.Price,
                Description = ItemDb.Description,
                Size        = ItemDb.Size,
                Color       = ItemDb.Color,
                ProductType = ItemDb.ProductType
            };

            return Item;
        }

        public List<ProductDto> GetAllProducts()
        {
            var ItemList = productRepository.GetAll().Select(x => new ProductDto()
            {
                ProductId   = x.ProductId,
                ProductName = x.ProductName,
                ProveedorId = x.ProveedorId,
                Price       = x.Price,
                Description = x.Description,
                Size        = x.Size,
                Color       = x.Color,
                ProductType = x.ProductType
            }).ToList();

            return ItemList;
        }

        // ---------------------------------------------------- Update Method
        public string UpdateProduct(ProductDto ObjModel)
        {
            string result = string.Empty;
            try
            {
                Products prod = productRepository.SingleOrDefault(x => x.ProductId == ObjModel.ProductId);
                if (prod != null)
                {
                    prod.ProductName = ObjModel.ProductName;
                    prod.ProveedorId = ObjModel.ProveedorId;
                    prod.Price       = ObjModel.Price;
                    prod.Description = ObjModel.Description;
                    prod.Size        = ObjModel.Size;
                    prod.Color       = ObjModel.Color;
                    prod.ProductType = ObjModel.ProductType;
                    productRepository.Update(prod);
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
        public string DeleteProduct(int id)
        {
            string result = string.Empty;
            try
            {
                productRepository.Delete(x => x.ProductId == id);
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
