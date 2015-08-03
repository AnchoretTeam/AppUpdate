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
    public sealed class WorkspaceModule : IModule
    {
        private readonly IUnityContainer _container;
        private readonly IRegionManager _regionManager;
        public WorkspaceModule(IUnityContainer container, IRegionManager regionManager)
        {
            _container = container;
            _regionManager = regionManager;
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
            _regionManager.RegisterViewWithRegion("WorkspaceRegion", typeof(ClientInfoView));
            _regionManager.RegisterViewWithRegion("WorkspaceRegion", typeof(ClientUpdateView));
            //_regionManager.RequestNavigate("WorkspaceRegion", typeof (ClientUpdateView).FullName);
        }
    }
}
