using AppUpdateServer.Models;
using Microsoft.Practices.Prism.PubSubEvents;

namespace AppUpdateServer.Events
{
    /// <summary>
    /// 指定一个项在列表框中选定
    /// </summary>
    public sealed class SpecifyAnAppDefinitionItemToSelectEvent : PubSubEvent<IAppDefinitionItem>
    {
    }
}
