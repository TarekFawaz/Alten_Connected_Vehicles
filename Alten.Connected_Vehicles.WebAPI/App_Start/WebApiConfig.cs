using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using DryIoc;
using DryIoc.WebApi;
using System.Data.Entity;
using Alten.Connected_Vehicles.DAL;
using Alten.Connected_Vehicles.Infrastructure.UnitOfWork;
using Alten.Connected_Vehicles.MSSQLRepository;
using Alten.Connected_Vehicles.Infrastructure.Repository;
using Alten.Connected_Vehicles.BLL.Interfaces;
using Alten.Connected_Vehicles.BLL.Services;
using System.Web.Http.Cors;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;

namespace Alten.Connected_Vehicles.WebAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {

            ///////////////////// DI based on DRY IOC/////////////////////////////
            var container = new Container(rules =>
    rules.With(FactoryMethod.ConstructorWithResolvableArguments))
              
              .WithWebApi(config, throwIfUnresolved: type => type.IsController());

           

            container.Register<DbContext, Alten_Connected_VehiclesEntities>(Reuse.Singleton);

            container.Register(typeof(IRepository<>), typeof(MSSQLRepository<>), setup: Setup.With(allowDisposableTransient: true));
            container.Register(typeof(IUnitOfWork<>),typeof(UnitOfWork<>), setup: Setup.With(allowDisposableTransient: true));
           

            container.Register<ICustomerService, CustomerService>();
            container.Register<IVehicleService, VehicleService>();

            /////////////End of DI Configuration ////////////////////////

            // Web API configuration and services
            var defaultCors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(defaultCors);

            /////////// 
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.MediaTypeMappings.Add(
    new QueryStringMapping("type", "json", new MediaTypeHeaderValue("application/json")));


            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
