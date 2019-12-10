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
    public class EnterpriseInfoBusiness : IEnterpriseInfoBusiness
    {
        private readonly IUnitOfWork unitOfwork;
        private readonly EnterpriseInfoRepository enterpriseInfoRepository;

        public EnterpriseInfoBusiness(IUnitOfWork _unitOfwork)
        {
            unitOfwork = _unitOfwork;
            enterpriseInfoRepository = new EnterpriseInfoRepository(unitOfwork);

        }

        
        //Get Enterprise Information From ADO Entity Context
        //public EnterpriseInfoDto GetEnterpriseInfo()
        //{
        //    EntitiesDB dbContext = new EntitiesDB();
        //    EnterpriseInfoDto EnterpriseInfo = dbContext.EnterpriseInformation.Select(r => new EnterpriseInfoDto()
        //    {
        //        //Address = "TEXTO TEST",//r.Address,
        //        //Email = "TEXTO TEST",//r.Email,
        //        //Phone = "TEXTO TEST",//r.Phone,
        //        //BusinessHours = "TEXTO TEST",//r.BusinessHours,
        //        //SocialMedia = "TEXTO TEST",//r.SocialMedia,
        //        //Mision = "TEXTO TEST",//r.Mision,
        //        //Valores = "TEXTO TEST",//r.Valores,
        //        //EmailOptional1 = "TEXTO TEST",//r.EmailOptional1,
        //        //EmailOptional2 = "TEXTO TEST",//r.EmailOptional2

        //        Address = r.Address,
        //        Email = r.Email,
        //        Phone = r.Phone,
        //        BusinessHours = r.BusinessHours,
        //        SocialMedia = r.SocialMedia,
        //        Mision = r.Mision,
        //        Vision = r.Vision,
        //        Valores = r.Valores,
        //        EmailOptional1 = r.EmailOptional1,
        //        EmailOptional2 = r.EmailOptional2
        //    }).FirstOrDefault();

        //    return EnterpriseInfo;
        //    //throw new NotImplementedException();
        //}

        //Get Enterprise Information From Repository Context
        public EnterpriseInfoDto GetEnterpriseInfo()
        {
            EnterpriseInfoDto EnterpriseInfo = enterpriseInfoRepository.GetAll().Select(r => new EnterpriseInfoDto()
            {
                Address = r.Address,
                Email = r.Email,
                Phone = r.Phone,
                BusinessHours = r.BusinessHours,
                SocialMedia = r.SocialMedia,
                Mision = r.Mision,
                Vision = r.Vision,
                Valores = r.Valores,
                EmailOptional1 = r.EmailOptional1,
                EmailOptional2 = r.EmailOptional2
            }).FirstOrDefault();
            return EnterpriseInfo;
            //throw new NotImplementedException();
        }


    }
}
