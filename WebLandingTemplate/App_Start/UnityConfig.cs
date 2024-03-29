using System.Web.Mvc;
using Unity;
using Unity.Injection;
using Unity.Mvc5;
using WebLandingTemplate.Controllers;
using WebLandingTemplateBusinessLogic.Interface;
using WebLandingTemplateBusinessLogic.Logic;
using WebLandingTemplateRepository.Infrastructure;
using WebLandingTemplateRepository.Infrastructure.Contract;

namespace WebLandingTemplate
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            
            // e.g. container.RegisterType<ITestService, TestService>();
            
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));

            //User Authentification.
            container.RegisterType<AccountController>(new InjectionConstructor());
            container.RegisterType<ManageController>(new InjectionConstructor());

            //Business Logic.
            container.RegisterType<IEnterpriseInfoBusiness, EnterpriseInfoBusiness>();
            container.RegisterType<IProductBusiness, ProductBusiness>();
            container.RegisterType<ISupplierBusiness, SupplierBusiness>();
            container.RegisterType<ICategoryBusiness, CategoryBusiness>();
            container.RegisterType<IServiceCorpBusiness, ServiceCorpBusiness>();

            //Repository Layer IUnitOfWork
            container.RegisterType<IUnitOfWork, UnitOfWork>();









        }
    }
}