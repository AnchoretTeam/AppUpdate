using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppUpdateServer.Views;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;

namespace AppUpdateServer.Modules
{
    public sealed class ClientListModule : IModule
    {
        private readonly IUnityContainer _container;
        private readonly IRegionViewRegistry _regionViewRegistry;
        public ClientListModule(IUnityContainer container, IRegionViewRegistry registry)
        {
            _container = container;
            _regionViewRegistry = registry;
        }

        public void Initialize()
        {
            RegisterServices();
            RegisterViews();
        }

        private void RegisterServices()
        {
            //_container.RegisterType<ITextService, TextService>();
        }

        private void RegisterViews()
        {
            _regionViewRegistry.RegisterViewWithRegion("ClientListRegion", typeof(ClientsTreeView));
        }
    }
}
