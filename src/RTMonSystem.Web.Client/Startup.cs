using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using Microsoft.Practices.Unity.Mvc;
using Owin;
using RTMonSystem.Interfaces;
using RTMonSystem.Web.Client.Hubs;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

[assembly: OwinStartup(typeof(RTMonSystem.Web.Client.Startup))]
namespace RTMonSystem.Web.Client
{
    public class SignalRUnityDependencyResolver : DefaultDependencyResolver
    {
        private IUnityContainer _container;

        public SignalRUnityDependencyResolver(IUnityContainer container)
        {
            _container = container;
        }

        public override object GetService(Type serviceType)
        {
            if (_container.IsRegistered(serviceType)) return _container.Resolve(serviceType);
            else return base.GetService(serviceType);
        }

        public override IEnumerable<object> GetServices(Type serviceType)
        {
            if (_container.IsRegistered(serviceType)) return _container.ResolveAll(serviceType);
            else return base.GetServices(serviceType);
        }

    }

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var container = BuildUnityContainer();
            var resolver = new UnityDependencyResolver(container);
            DependencyResolver.SetResolver(resolver);
            GlobalHost.DependencyResolver = new SignalRUnityDependencyResolver(container);
            
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

            container.RegisterType<WidgetHub>(new InjectionFactory(c => new WidgetHub(c.Resolve<IWorkersManager>())));

            return container;
        }
    }
}