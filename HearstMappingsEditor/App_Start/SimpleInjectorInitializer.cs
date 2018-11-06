[assembly: WebActivator.PostApplicationStartMethod(typeof(HearstMappingsEditor.App_Start.SimpleInjectorInitializer), "Initialize")]

namespace HearstMappingsEditor.App_Start
{
    using System.Reflection;
    using System.Web.Mvc;
    using HearstMappingsEditor.Common;
    using HearstMappingsEditor.Common.Interfaces;
    using HearstMappingsEditor.Data.Repository.Ef;
    using HearstMappingsEditor.Data.Repository.Interfaces;
    using SimpleInjector;
    using SimpleInjector.Integration.Web;
    using SimpleInjector.Integration.Web.Mvc;
    
    public static class SimpleInjectorInitializer
    {
        public static Container Container;

        public static void Initialize()
        {
            Container = new Container();
            Container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();
            
            InitializeContainer(Container);

            Container.RegisterMvcControllers(Assembly.GetExecutingAssembly());

            Container.Verify();
            
            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(Container));
        }
     
        private static void InitializeContainer(Container container)
        {
            container.Register<ILogger, Logger>(Lifestyle.Singleton);
            container.Register<ICache, InMemoryCache>(Lifestyle.Singleton);
            container.Register<ISettingsManager, SettingsManager>(Lifestyle.Singleton);
            container.Register<IRestrictionsRepository, RestrictionsRepository>(Lifestyle.Singleton);
            container.Register<IReferencesRepository, ReferencesRepository>(Lifestyle.Singleton);
            container.Register<IMappingSyncLogic, MappingSyncLogic>(Lifestyle.Singleton);
            container.Register(typeof(IMappingRepository<,,>), typeof(ReferencesRepository).Assembly, Lifestyle.Singleton);
            container.Register(typeof(IReferenceRepository<,>), typeof(ReferencesRepository).Assembly, Lifestyle.Singleton);
        }
    }
}