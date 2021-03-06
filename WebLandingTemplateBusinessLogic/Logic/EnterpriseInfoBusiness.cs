﻿using System;
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
    public class EnterpriseInfoBusiness : IEnterpriseInfoBusiness
    {
        private readonly IUnitOfWork unitOfwork;
        private readonly EnterpriseInfoRepository enterpriseInfoRepository;

        public EnterpriseInfoBusiness(IUnitOfWork _unitOfwork)
        {
            unitOfwork = _unitOfwork;
            enterpriseInfoRepository = new EnterpriseInfoRepository(unitOfwork);

        }

        #region Enterprise Information CRUD

        // ---------------------------------------------------- Create Method
        //public string InsertItem(FrontDashboardDto ObjModel)
        //{
        //    string result = string.Empty;
        //    try
        //    {
        //        Dashboard NewItem = new Dashboard()
        //        {
        //            //ItemImageId = ObjModel.ItemImageId, -> key
        //            ElementName = ObjModel.ElementName,
        //            ElementId = ObjModel.ElementId
        //        };
        //        var record = frontRepository.Insert(NewItem);
        //        result = (record == null) ? "Failed" : "Succes";
        //    }
        //    catch (Exception ex)
        //    {
        //        result = "Error: " + ex.Message;
        //    }
        //    return result;
        //}

        // ---------------------------------------------------- Retrive/Get Method
        public EnterpriseInfoDto GetEnterpriseInfo()
        {
            EnterpriseInfoDto EnterpriseInfo = enterpriseInfoRepository.GetAll().Select(r => new EnterpriseInfoDto()
            {
                IdInfo = r.IdInfo,
                Address = r.Address,
                Email = r.Email,
                Phone = r.Phone,
                BusinessHours = r.BusinessHours,
                SocialMedia = r.SocialMedia,
                Mision = r.Mision,
                Vision = r.Vision,
                Valores = r.Valores,
                EmailOptional1 = r.EmailOptional1,
                EmailOptional2 = r.EmailOptional2,
                Logo = r.Logo,
                PromotionalText1 = r.PromotionalText1,
                PromotionalText2 = r.PromotionalText2,
                PromotionalText3 = r.PromotionalText3,
                PromotionalText4 = r.PromotionalText4,

            }).FirstOrDefault();
            return EnterpriseInfo;
            //throw new NotImplementedException();
        }

        // ---------------------------------------------------- Update Method
        public string UpdateItem(EnterpriseInfoDto ObjModel)
        {
            string result = string.Empty;
            try
            {
                EnterpriseInformation item = enterpriseInfoRepository.SingleOrDefault(x => x.Address == ObjModel.Address);
                if (item != null)
                {
                    item.Address = ObjModel.Address;
                    item.Email = ObjModel.Email;
                    item.Phone = ObjModel.Phone;
                    item.BusinessHours = ObjModel.BusinessHours;
                    item.SocialMedia = ObjModel.SocialMedia;
                    item.Mision = ObjModel.Mision;
                    item.Vision = ObjModel.Vision;
                    item.Valores = ObjModel.Valores;
                    item.EmailOptional1 = ObjModel.EmailOptional1;
                    item.EmailOptional2 = ObjModel.EmailOptional2;
                    item.PromotionalText1 = ObjModel.PromotionalText1;
                    item.PromotionalText2 = ObjModel.PromotionalText2;
                    item.PromotionalText3 = ObjModel.PromotionalText3;
                    item.PromotionalText4 = ObjModel.PromotionalText4;
                    item.Logo = ObjModel.Logo;
                    enterpriseInfoRepository.Update(item);
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
        //public string DeleteItem(int id)
        //{
        //    string result = string.Empty;
        //    try
        //    {
        //        frontRepository.Delete(x => x.ItemImageId == id);
        //        result = "Succes";
        //    }
        //    catch (Exception ex)
        //    {
        //        result = "Error: " + ex.Message;
        //    }
        //    return result;
        //}

        #endregion


        
        


    }
}
