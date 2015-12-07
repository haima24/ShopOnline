using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using ShopOnline.App_Start;
using ShopOnline.Extension;

namespace ShopOnline
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            AutoMapperConfig.RegisterMappings();
            ////find the default JsonVAlueProviderFactory
            //JsonValueProviderFactory jsonValueProviderFactory = null;
            //foreach (var factory in ValueProviderFactories.Factories)
            //{
            //    if (factory is JsonValueProviderFactory)
            //    {
            //        jsonValueProviderFactory = factory as JsonValueProviderFactory;
            //    }
            //}

            ////remove the default JsonVAlueProviderFactory
            //if (jsonValueProviderFactory != null) ValueProviderFactories.Factories.Remove(jsonValueProviderFactory);

            ////add the custom one
            //ValueProviderFactories.Factories.Add(new CustomJsonValueProviderFactory());
        }
    }
}