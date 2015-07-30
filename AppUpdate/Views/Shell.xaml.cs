using System.Windows;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.PubSubEvents;

namespace AppUpdate.Views
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class Shell : Window, IView
    {
        private readonly IEventAggregator _aggregator;

        public Shell(IEventAggregator aggregator)
        {
            InitializeComponent();
            _aggregator = aggregator;
        }
    }
}
