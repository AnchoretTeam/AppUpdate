using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Practices.Prism.Mvvm;
using System.Reflection;

namespace AppUpdateServer
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            //设置ViewModel与View自动关联逻辑
            ViewModelLocationProvider.SetDefaultViewTypeToViewModelTypeResolver(viewType =>
            {
                var viewName = viewType.FullName;
                viewName = viewName.Replace(".Views.", ".ViewModels.");
                var viewAssemblyName = viewType.GetTypeInfo().Assembly.FullName;
                var viewModelName = string.Format(CultureInfo.InvariantCulture, "{0}ViewModel, {1}", viewName, viewAssemblyName);
                var viewModelType = Type.GetType(viewModelName);
                if (viewModelType == null && viewName.EndsWith("View", StringComparison.OrdinalIgnoreCase))
                {
                    viewModelName = string.Format(CultureInfo.InvariantCulture, "{0}Model, {1}", viewName, viewAssemblyName);
                    viewModelType = Type.GetType(viewModelName);
                }

                return viewModelType;
            });

            var bootstrapper = new Bootstrapper();
            bootstrapper.Run();
        }
    }
}
