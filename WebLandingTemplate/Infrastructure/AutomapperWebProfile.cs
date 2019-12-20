using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebLandingTemplate.Models;
using WebLandingTemplateDomainModel.Models;

namespace WebLandingTemplate.Infrastructure
{
    public class AutomapperWebProfile : AutoMapper.Profile
    {
        public AutomapperWebProfile()
        {

            CreateMap<SupplierDto, SupplierVM>();
            CreateMap<SupplierVM, SupplierDto>();

            CreateMap<CategoryDto, CategoryVM>();
            CreateMap<CategoryVM, CategoryDto>();

            CreateMap<FrontDashboardDto, FrontDashboardVM>();
            CreateMap<FrontDashboardVM, FrontDashboardDto>();

            CreateMap<ProductDto, ProductVM>();
            CreateMap<ProductVM, ProductDto>();

            CreateMap<ServiceCorpDto, ServiceCorpVM>();
            CreateMap<ServiceCorpVM, ServiceCorpDto>();

            CreateMap<MessageDto, ContactMessageVM>();
            CreateMap<ContactMessageVM, MessageDto>();

            CreateMap<ImageSrcDto, ImageSrcVM>();
            CreateMap<ImageSrcVM, ImageSrcDto>();

            CreateMap<EnterpriseInfoDto, EnterpriseInfoVM>();
            CreateMap<EnterpriseInfoVM, EnterpriseInfoDto>();

        }

        public static void Run()
        {
            AutoMapper.Mapper.Initialize(a =>
            {
                a.AddProfile<AutomapperWebProfile>();


            });
        }



    }
}