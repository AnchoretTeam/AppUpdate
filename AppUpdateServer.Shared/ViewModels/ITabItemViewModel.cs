using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AppUpdateServer.ViewModels
{
    public interface ITabItemViewModel
    {
        object TabContent { get; }
        Visibility Visibility { get; }
    }
}
