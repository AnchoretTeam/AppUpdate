
using AppUpdateServer.Models;
using Microsoft.Practices.Prism.PubSubEvents;

namespace AppUpdateServer.Events
{
    /// <summary>
    /// 列表中选择的项改变时通知
    /// </summary>
    public sealed class SelectedAppDefinitionItemChangedEvent : PubSubEvent<IAppDefinitionItem>
    {
    }
}
