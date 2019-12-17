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

            CreateMap<ProductDto, ProductVM>();
            CreateMap<ProductVM, ProductDto>();

            CreateMap<CategoryDto, CategoryVM>();
            CreateMap<CategoryVM, CategoryDto>();

            CreateMap<ServiceCorpDto, ServiceCorpVM>();
            CreateMap<ServiceCorpVM, ServiceCorpDto>();

            CreateMap<SupplierDto,SupplierVM>();
            CreateMap<SupplierVM, SupplierDto>();

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