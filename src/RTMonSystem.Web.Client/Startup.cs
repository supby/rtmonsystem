using Microsoft.Owin;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using Microsoft.Practices.Unity.Mvc;
using Owin;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

[assembly: OwinStartup(typeof(RTMonSystem.Web.Client.Startup))]
namespace RTMonSystem.Web.Client
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var container = BuildUnityContainer();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));

            app.MapSignalR();
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers
            // register IoC
            var config = ConfigurationManager.OpenMappedExeConfiguration
            (
                new ExeConfigurationFileMap
                {
                    ExeConfigFilename = System.IO.Path.Combine(HttpRuntime.AppDomainAppPath, "unity.config")
                },
                ConfigurationUserLevel.None
            );
            container.LoadConfiguration((UnityConfigurationSection)config.GetSection("unity"));

            return container;
        }
    }
}